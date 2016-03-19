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

class Game
{
public:
	static const int cellWidth = 10;
	static const int cellHeight = 10;
	static const int cellsInColumn = 40;
	static const int cellsInRow = 40;
	static const int frameTimeMlsc = 50;
	static const int directionChangeChangePercent = 10;
};

//Klasa do przekazywania wê¿a miêdzy oknami poprzez int
class SnakeTransitionInformation
{
public:
	//od 12, d³ugoœæ 8 bit
	int segmentsCount;
	//od 4, d³ugoœæ 8 bit
	int position;
	//od 3, d³ugoœæ 1 bit
	bool isSteerable;
	//od 0, d³ugoœæ 3 bit
	Direction direction;

	SnakeTransitionInformation() {}

	SnakeTransitionInformation(int toParse)
	{
		direction = (Direction)(toParse & 0x00000003);
		isSteerable = (bool)((toParse >> 3) & 0x00000001);
		position = (toParse >> 4) & 0x000000FF;
		segmentsCount = (toParse >> 12) & 0x000000FF;
	}

	int ParseToInt()
	{
		return (int)direction |
			(((int)isSteerable) << 3) |
			(position << 4) |
			(segmentsCount << 12);
	}
};

class SnakeSegment
{
public:
	//wartoœæ logiczna na szachownicy o polach o wielkoœci width na height
	int positionX;
	int positionY;

	void Draw(HDC* hdc, bool visible)
	{
		HPEN hPen;
		HBRUSH hBrush;
		if (visible)
		{
			hPen = CreatePen(PS_SOLID, 1, RGB(0, 0, 255));
			hBrush = CreateSolidBrush(RGB(0, 0, 255));
		}
		else
		{
			hPen = CreatePen(PS_SOLID, 1, RGB(0, 0, 0));
			hBrush = CreateSolidBrush(RGB(0, 0, 0));
		}
		SelectObject(*hdc, hPen);
		SelectObject(*hdc, hBrush);
		int xPx = positionX * Game::cellWidth;
		int yPx = positionY * Game::cellHeight;
		Rectangle(*hdc, xPx, yPx, xPx + Game::cellWidth, yPx + Game::cellHeight);
		DeleteObject(hPen);
		DeleteObject(hBrush);
	}
};

class Snake
{
public:
	std::deque<SnakeSegment*> Segments;
	Direction currentDirection;
	int segmentsToGrow;

	//Used for creting snake in the middle of screen
	Snake(Direction direction, int length)
	{
		currentDirection = direction;
		SnakeSegment* head = new SnakeSegment();
		Segments.push_back(head);
		head->positionX = Game::cellsInRow / 2;
		head->positionY = Game::cellsInColumn / 2;
		segmentsToGrow = length - 1;
	}

	//Used for creating snake after transition
	Snake(Direction direction, int position, int length)
	{
		segmentsToGrow = length - 1;
		currentDirection = direction;
		SnakeSegment* head = new SnakeSegment();
		Segments.push_back(head);
		if (currentDirection == Direction::LEFT) {
			head->positionX = Game::cellsInRow;
			head->positionY = position;
		}
		else if (currentDirection == Direction::RIGHT)
		{
			head->positionX = 0;
			head->positionY = position;
		}
		if (currentDirection == Direction::UP) {
			head->positionX = position;
			head->positionY = Game::cellsInColumn;
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
		if (segmentsToGrow < 0 || Segments.size() == 0)
		{
			//not moving, already transitioning or dying
			return;
		}
		SnakeSegment* head = Segments.front();
		SnakeTransitionInformation transactionInfo;
		if (head->positionX > Game::cellsInRow || head->positionX < 0)
		{
			transactionInfo.position = head->positionY;
		}
		else if (head->positionY > Game::cellsInColumn || head->positionY < 0)
		{
			transactionInfo.position = head->positionY;
		}
		else
		{
			return;
		}
		transactionInfo.direction = currentDirection;
		transactionInfo.isSteerable = (this == mainSnake);
		transactionInfo.segmentsCount = Segments.size();
		PostMessage(hwndOtherWindow, windowMessageSnakeTransaction, (WPARAM)transactionInfo.ParseToInt(), NULL);
		this->segmentsToGrow = -(int)Segments.size();
	}
private:
	void TryShortenSnake(HDC* hdc)
	{
		if (segmentsToGrow <= 0)
		{
			Segments[Segments.size() - 1]->Draw(hdc, false);
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
			SnakeSegment* segment = new SnakeSegment();
			segment->positionX = head.positionX + directionChangeX[currentDirection];
			segment->positionY = head.positionY + directionChangeY[currentDirection];
			Segments.push_front(segment);
			Segments[0]->Draw(hdc, true);
		}
		else
		{
			segmentsToGrow++;
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
		if (rand() % 100 < Game::directionChangeChangePercent)
		{
			Snakes[i]->TryChangeDirection((Direction)(rand() % 4));
		}
		Snakes[i]->Move(hdc);
		Snakes[i]->TransitionToSecondWindowIfNeeded();
		if (Snakes[i]->Segments.size() == 0)
		{
			delete Snakes[i];
			Snakes.erase(Snakes.begin() + i);
		}
	}
}

void InitializeSnakes()
{
	Direction direction = (Direction)(rand() % 4);
	mainSnake = new Snake(direction, 10);
	Snakes.push_back(mainSnake);
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
		Game::cellWidth * Game::cellsInRow, 
		Game::cellHeight * Game::cellsInColumn,
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
		return 0;
	}
	if (message == windowMessageHelloReply)
	{
		hwndOtherWindow = (HWND)wParam;
		InitializeSnakes();
		return 0;
	}
	if (message == windowMessageSnakeTransaction)
	{
		SnakeTransitionInformation s(wParam);
		Snake* snake = new Snake(s.direction, s.position, s.segmentsCount);
		if (s.isSteerable)
		{
			mainSnake = snake;
		}
		Snakes.push_back(snake);
		return 0;
	}
	
	PAINTSTRUCT ps;
	HDC hdc;
	switch (message)
	{
	case WM_TIMER:
		hdc = GetDC(hwnd);
		MoveSnakes(&hdc);
		ReleaseDC(hwnd, hdc);
		break;
	case WM_PAINT:
	case WM_CREATE:
		hdc = BeginPaint(hwnd, &ps);
		DrawSnakes(&hdc);
		EndPaint(hwnd, &ps);
		return 0;
	case WM_DESTROY:
		PostQuitMessage(0);
		return 0;
	}
	return DefWindowProc(hwnd, message, wParam, lParam);
}