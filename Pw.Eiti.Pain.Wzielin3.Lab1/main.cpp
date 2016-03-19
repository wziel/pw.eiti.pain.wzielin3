#include<Windows.h>
#include<deque>
#include<vector>
#include<time.h>
LONG WINAPI WndProc(HWND, UINT, WPARAM, LPARAM);

enum Direction { LEFT = 0, RIGHT = 1, UP = 2, DOWN = 3 };
int directionChangeX[4] = { -1,1,0,0 };
int directionChangeY[4] = { 0,0,-1,1 };

class SnakeSegment
{
public:
	static const int width = 10;
	static const int height = 10;

	//described in column and row, not in pixels
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
		int xPx = positionX * SnakeSegment::width;
		int yPx = positionY * SnakeSegment::height;
		Rectangle(*hdc, xPx, yPx, xPx + width, yPx + height);
		DeleteObject(hPen);
		DeleteObject(hBrush);
	}
};

class Snake
{
public:
	std::deque<SnakeSegment*> Segments;
	Direction currentDirection;
	int segmentsToGrow = 5;

	Snake()
	{
		currentDirection = Direction::RIGHT;
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

std::vector<Snake*> Snakes;

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
		if (rand() % 100 < 30)
		{
			Snakes[i]->TryChangeDirection((Direction)(rand() % 4));
		}
		Snakes[i]->Move(hdc);
	}
}

void InitializeSnakes()
{
	Snake* snake = new Snake();
	SnakeSegment* head = new SnakeSegment();
	head->positionX = 20;
	head->positionY = 20;
	snake->Segments.push_back(head);
	Snakes.push_back(snake);
}


int WINAPI WinMain(HINSTANCE hinstance, HINSTANCE hPrevInstance, LPSTR lpszCmdLine, int nCmdShow)
{
	srand(time(NULL));
	InitializeSnakes();

	static char szAppName[] = "Gniazdo ¿mij";
	static const int windowSize = 400;
	WNDCLASS wndClass;
	HWND hwnd;
	MSG msg;
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
		CW_USEDEFAULT, CW_USEDEFAULT,
		windowSize, windowSize,
		HWND_DESKTOP,
		NULL,
		hinstance,
		NULL);

	ShowWindow(hwnd, nCmdShow);
	UpdateWindow(hwnd);

	SetTimer(hwnd, ID_TIMER, 50, NULL);

	while (GetMessage(&msg, NULL, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return msg.wParam;
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	PAINTSTRUCT ps;
	HDC hdc;
	switch (message)
	{
	case WM_CREATE:
	case WM_PAINT:
		hdc = BeginPaint(hwnd, &ps);
		DrawSnakes(&hdc);
		EndPaint(hwnd, &ps);
		return 0;
	case WM_TIMER:
		hdc = GetDC(hwnd);
		MoveSnakes(&hdc);
		ReleaseDC(hwnd, hdc);
		break;
	case WM_DESTROY:
		PostQuitMessage(0);
		return 0;
	}
	return DefWindowProc(hwnd, message, wParam, lParam);
}