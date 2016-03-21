#include<Windows.h>
#include<deque>
#include<vector>
#include<time.h>
LONG WINAPI WndProc(HWND, UINT, WPARAM, LPARAM);
class Snake;

enum Direction { LEFT = 0, RIGHT = 1, UP = 2, DOWN = 3 };
int directionChangeX[4] = { -1,1,0,0 };
int directionChangeY[4] = { 0,0,-1,1 };
HWND hwndOtherWindow;
//hwnd okna przekazywany w wParam
int windowMessageHello = RegisterWindowMessage("Pw.Eiti.Pain.Wzielin3.Lab1.Hello");
//hwnd okna przekazywany w wParam
int windowMessageHelloReply = RegisterWindowMessage("Pw.Eiti.Pain.Wzielin3.Lab1.HelloReply");
//w wParam przekazywany stan wê¿a jako int. do obliczenia wykorzystywana klasa pomocnicza SnakeTransactionInformation
int windowMessageSnakeTransaction = RegisterWindowMessage("Pw.Eiti.Pain.Wzielin3.Lab1.SnakeTransaction");
std::vector<Snake*> Snakes;
Snake* mainSnake;

float GetPixelsPerMM_LOMETRIC()
{
	HDC screen = GetDC(NULL);
	int hSize = GetDeviceCaps(screen, HORZSIZE);
	int hRes = GetDeviceCaps(screen, HORZRES);
	float PixelsPerMM = (float)hRes / hSize;   // pixels per millimeter
	//return 1;
	return PixelsPerMM / 10;
}

class Game
{
public:
	static const int cellWidth = 20;
	static const int cellHeight = 20;
	static const int cellsInColumn = 80;
	static const int cellsInRow = 80;
	static const int frameTimeMlsc = 50;
	static const int directionChangeChangePercent = 10;
	static const int mainSnakeColor = RGB(0, 0, 255);
	static const int otherSnakesColor = RGB(0, 255, 0);
	static const int windowColor = RGB(0, 0, 0);
	static const int otherSnakesCount = 50;
	static const int snakeLength = 5;

};
//szachownica gry wskazuj¹ca na wê¿a który znajduje siê na danym polu
Snake* GameBoard[Game::cellsInColumn][Game::cellsInRow];

//Klasa do przekazywania wê¿a miêdzy oknami poprzez int
class SnakeTransitionInformation
{
public:
	//od 11, d³ugoœæ 8 bit
	int segmentsCount;
	//od 3, d³ugoœæ 8 bit
	int position;
	//od 2, d³ugoœæ 1 bit
	bool isSteerable;
	//od 0, d³ugoœæ 2 bit
	Direction direction;

	SnakeTransitionInformation() {}

	SnakeTransitionInformation(int toParse)
	{
		direction = (Direction)(toParse & 0x00000003);
		isSteerable = (bool)((toParse >> 2) & 0x00000001);
		position = (toParse >> 3) & 0x000000FF;
		segmentsCount = (toParse >> 11) & 0x000000FF;
	}

	int ParseToInt()
	{
		return (int)direction |
			(((int)isSteerable) << 2) |
			(position << 3) |
			(segmentsCount << 11);
	}
};

class SnakeSegment
{
public:
	//wartoœæ logiczna na szachownicy o polach o wielkoœci width na height
	int positionX;
	int positionY;

	SnakeSegment(bool isMainSnake)
	{
		backgroundColor = Game::windowColor;
		snakeColor = isMainSnake ? Game::mainSnakeColor : Game::otherSnakesColor;
	}

	void Draw(HDC* hdc, bool visible)
	{
		HPEN hPen;
		HBRUSH hBrush;
		if (visible)
		{
			hPen = CreatePen(PS_SOLID, 1, snakeColor);
			hBrush = CreateSolidBrush(snakeColor);
		}
		else
		{
			hPen = CreatePen(PS_SOLID, 1, backgroundColor);
			hBrush = CreateSolidBrush(backgroundColor);
		}
		SelectObject(*hdc, hPen);
		SelectObject(*hdc, hBrush);
		int x = positionX * Game::cellWidth;
		int y = -positionY * Game::cellHeight;
		Rectangle(*hdc, x, y, x + Game::cellWidth, y + Game::cellHeight);
		DeleteObject(hPen);
		DeleteObject(hBrush);
	}
private:
	int snakeColor;
	int backgroundColor;
};

class Snake
{
public:
	std::deque<SnakeSegment*> Segments;
	Direction currentDirection;
	int segmentsToGrow;

	//Used for creting snake in the middle of screen
	Snake(Direction direction, int length, bool isMainSnake)
		: currentDirection(direction), isMainSnake(isMainSnake), segmentsToGrow(length - 1)
	{
		SnakeSegment* head = new SnakeSegment(isMainSnake);
		Segments.push_back(head);
		head->positionX = rand() % Game::cellsInRow;
		head->positionY = rand() % Game::cellsInColumn;
		segmentsToGrow = length - 1;
	}

	//Used for creating snake after transition
	Snake(Direction direction, int position, int length, bool isMainSnake)
		: currentDirection(direction), isMainSnake(isMainSnake), segmentsToGrow(length - 1)
	{
		SnakeSegment* head = new SnakeSegment(isMainSnake);
		Segments.push_back(head);
		if (currentDirection == Direction::LEFT) {
			head->positionX = Game::cellsInRow - 1;
			head->positionY = position;
		}
		else if (currentDirection == Direction::RIGHT)
		{
			head->positionX = 0;
			head->positionY = position;
		}
		if (currentDirection == Direction::UP) {
			head->positionX = position;
			head->positionY = Game::cellsInColumn - 1;
		}
		else if (currentDirection == Direction::DOWN)
		{
			head->positionX = position;
			head->positionY = 0;
		}
	}

	~Snake()
	{
		while (Segments.size() > 0)
		{
			delete Segments[Segments.size() - 1];
			Segments.pop_back();
		}
	}

	void Draw(HDC* hdc)
	{
		std::deque<SnakeSegment*>::iterator it = Segments.begin();

		while (it != Segments.end())
		{
			(*it)->Draw(hdc, true);
			it++;
		}
	}

	void Move(HDC* hdc)
	{
		if (Segments.size() > 0)
		{
			TryShortenSnake(hdc);
			TryExtendSnake(hdc);
		}
		Draw(hdc);
	}

	void TryChangeDirection(Direction direction)
	{
		if (direction != currentDirection &&
			abs(directionChangeX[direction]) + abs(directionChangeX[currentDirection]) < 2 &&
			abs(directionChangeY[direction]) + abs(directionChangeY[currentDirection]) < 2)
		{
			currentDirection = direction;
		}
	}

	void TransitionToSecondWindowIfNeeded()
	{
		if (segmentsToGrow < 0 || isDead)
		{
			//not moving, already transitioning or dying
			return;
		}
		SnakeSegment* head = Segments.front();
		SnakeTransitionInformation transactionInfo;
		if ((head->positionX == Game::cellsInRow - 1 && currentDirection == Direction::RIGHT) ||
			(head->positionX == 0 && currentDirection == Direction::LEFT))
		{
			transactionInfo.position = head->positionY;
		}
		else if ((head->positionY == Game::cellsInColumn - 1 && currentDirection == Direction::DOWN) ||
			(head->positionY == 0 && currentDirection == Direction::UP))
		{
			transactionInfo.position = head->positionX;
		}
		else
		{
			return;
		}
		transactionInfo.direction = currentDirection;
		transactionInfo.isSteerable = (this == mainSnake);
		if (transactionInfo.isSteerable)
		{
			mainSnake = NULL;
			BringWindowToTop(hwndOtherWindow);
		}
		transactionInfo.segmentsCount = Segments.size() + segmentsToGrow;
		PostMessage(hwndOtherWindow, windowMessageSnakeTransaction, (WPARAM)transactionInfo.ParseToInt(), NULL);
		Kill();
	}

	bool isDead = false;
	void Kill()
	{
		isDead = true;
		segmentsToGrow = -(int)Segments.size();
	}

private:
	bool isMainSnake;

	void TryShortenSnake(HDC* hdc)
	{
		if (segmentsToGrow <= 0)
		{
			SnakeSegment* segment = Segments[Segments.size() - 1];
			ReleaseCell(segment->positionX, segment->positionY);
			segment->Draw(hdc, false);
			delete Segments[Segments.size() - 1];
			Segments.pop_back();
		}
		else
		{
			segmentsToGrow--;
		}
	}

	void TryExtendSnake(HDC* hdc)
	{
		if (segmentsToGrow >= 0)
		{
			SnakeSegment head = *Segments.front();
			SnakeSegment* segment = new SnakeSegment(isMainSnake);
			segment->positionX = head.positionX + directionChangeX[currentDirection];
			segment->positionY = head.positionY + directionChangeY[currentDirection];
			TakeOverCell(segment->positionX, segment->positionY);
			Segments.push_front(segment);
			head = segment;
			Segments[0]->Draw(hdc, true);
		}
		else
		{
			segmentsToGrow++;
		}
	}

	void TakeOverCell(int x, int y)
	{
		Snake* current = GameBoard[x][y];
		if (current != NULL && current != this)
		{
			current->Kill();
		}
		GameBoard[x][y] = this;
	}

	void ReleaseCell(int x, int y)
	{
		Snake* current = GameBoard[x][y];
		if (current != NULL && current == this)
		{
			GameBoard[x][y] = NULL;
		}
	}
};

void DrawSnakes(HDC* hdc)
{
	for (int i = Snakes.size() - 1; i >= 0; --i)
	{
		Snakes[i]->Draw(hdc);
	}
}

void MoveSnakes(HDC* hdc)
{
	for (int i = Snakes.size() - 1; i >= 0; --i)
	{
		Snake* snake = Snakes[i];
		if (snake != mainSnake && rand() % 100 < Game::directionChangeChangePercent)
		{
			snake->TryChangeDirection((Direction)(rand() % 4));
		}
		snake->TransitionToSecondWindowIfNeeded();
		snake->Move(hdc);
		if (snake->Segments.size() == 0)
		{
			if (snake == mainSnake)
			{
				mainSnake = NULL;
			}
			delete snake;
			Snakes.erase(Snakes.begin() + i);
		}
	}
}

void InitializeMainSnake()
{
	Direction direction = (Direction)(rand() % 4);
	mainSnake = new Snake(direction, Game::snakeLength, true);
	Snakes.push_back(mainSnake);
}

void InitializeOtherSnakes()
{
	for (int i = Game::otherSnakesCount; i > 0; --i)
	{
		Direction direction = (Direction)(rand() % 4);
		Snake* snake = new Snake(direction, Game::snakeLength, false);
		Snakes.push_back(snake);
	}
}

void HandleKeyDown(int virtualKeyCode)
{
	if (mainSnake == NULL) return;
	if (virtualKeyCode == VK_LEFT) mainSnake->TryChangeDirection(Direction::LEFT);
	if (virtualKeyCode == VK_RIGHT) mainSnake->TryChangeDirection(Direction::RIGHT);
	if (virtualKeyCode == VK_UP) mainSnake->TryChangeDirection(Direction::UP);
	if (virtualKeyCode == VK_DOWN) mainSnake->TryChangeDirection(Direction::DOWN);
}

HWND InitializeWindow(HINSTANCE hinstance, HINSTANCE hPrevInstance, LPSTR lpszCmdLine, int nCmdShow)
{
	static char szAppName[] = "Gniazdo ¿mij";
	WNDCLASS wndClass;
	HWND hwnd;
	const WORD ID_TIMER = 1;
	wndClass.style = 0;
	wndClass.lpfnWndProc = (WNDPROC)WndProc;
	wndClass.cbClsExtra = 0;
	wndClass.cbWndExtra = 0;
	wndClass.hInstance = hinstance;
	wndClass.hIcon = LoadIcon(NULL, IDI_APPLICATION);
	wndClass.hCursor = LoadCursor(NULL, IDC_ARROW);
	wndClass.hbrBackground = (HBRUSH)COLOR_WINDOW + 1;
	wndClass.lpszMenuName = NULL;
	wndClass.lpszClassName = szAppName;
	RegisterClass(&wndClass);

	hwnd = CreateWindow(szAppName,
		szAppName,
		WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT,
		CW_USEDEFAULT,
		Game::cellWidth * Game::cellsInRow *GetPixelsPerMM_LOMETRIC(),
	Game::cellHeight * Game::cellsInColumn *GetPixelsPerMM_LOMETRIC(),
		HWND_DESKTOP,
		NULL,
		hinstance,
		NULL);

	ShowWindow(hwnd, nCmdShow);
	UpdateWindow(hwnd);
	SetTimer(hwnd, ID_TIMER, Game::frameTimeMlsc, NULL);

	return hwnd;
}


int WINAPI WinMain(HINSTANCE hinstance, HINSTANCE hPrevInstance, LPSTR lpszCmdLine, int nCmdShow)
{
	srand(time(NULL));
	HWND hwnd = InitializeWindow(hinstance, hPrevInstance, lpszCmdLine, nCmdShow);
	PostMessage(HWND_BROADCAST, windowMessageHello, (WPARAM)hwnd, NULL);

	MSG msg;
	while (GetMessage(&msg, NULL, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return msg.wParam;
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	if (message == windowMessageHello && (WPARAM)hwnd != wParam)
	{
		hwndOtherWindow = (HWND)wParam;
		PostMessage(hwndOtherWindow, windowMessageHelloReply, (WPARAM)hwnd, NULL);
		InitializeOtherSnakes();
		return 0;
	}
	if (message == windowMessageHelloReply)
	{
		hwndOtherWindow = (HWND)wParam;
		InitializeMainSnake();
		return 0;
	}
	if (message == windowMessageSnakeTransaction)
	{
		SnakeTransitionInformation s(wParam);
		Snake* snake = new Snake(s.direction, s.position, s.segmentsCount, s.isSteerable);
		if (s.isSteerable)
		{
			mainSnake = snake;
		}
		Snakes.push_back(snake);
		return 0;
	}

	RECT rc;
	PAINTSTRUCT ps;
	HDC hdc;
	switch (message)
	{
	case WM_TIMER:
		hdc = GetDC(hwnd);
		GetClientRect(hwnd, &rc);
		SetMapMode(hdc, MM_LOMETRIC);
		MoveSnakes(&hdc);
		ReleaseDC(hwnd, hdc);
		break;
	case WM_KEYDOWN:
		HandleKeyDown(wParam);
		return 0;
	case WM_PAINT:
	case WM_CREATE:
		hdc = BeginPaint(hwnd, &ps);
		GetClientRect(hwnd, &rc);
		SetMapMode(hdc, MM_LOMETRIC);
		DrawSnakes(&hdc);
		EndPaint(hwnd, &ps);
		return 0;
	case WM_DESTROY:
		PostQuitMessage(0);
		return 0;
	}
	return DefWindowProc(hwnd, message, wParam, lParam);
}