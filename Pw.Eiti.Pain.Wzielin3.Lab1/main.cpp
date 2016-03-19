#include<Windows.h>
#include<deque>
#include<vector>
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
			hPen = CreatePen(PS_SOLID, 1, RGB(255, 0, 0));
			hBrush = CreateSolidBrush(RGB(255, 0, 0));
		}
		else
		{
			hPen = CreatePen(PS_SOLID, 1, RGB(10, 20, 30));
			hBrush = CreateSolidBrush(RGB(10, 20, 30));
		}
		SelectObject(*hdc, hPen);
		SelectObject(*hdc, hBrush);
		Rectangle(*hdc, positionX, positionY, positionX + width, positionY + height);
		DeleteObject(hPen);
		DeleteObject(hBrush);
	}
};

class Snake
{
public:
	std::deque<SnakeSegment*> Segments;
	Direction currentDirection;

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

	void Move(HDC* hdc, Direction direction)
	{
		Segments[Segments.size() - 1]->Draw(hdc, false);

		SnakeSegment head = *Segments.front();

		delete Segments[Segments.size() - 1];
		Segments.pop_back();

		SnakeSegment* segment = new SnakeSegment();
		segment->positionX = (head.positionX + directionChangeX[direction]) * SnakeSegment::width;
		segment->positionY = (head.positionY * directionChangeY[direction]) * SnakeSegment::height;
		Segments.push_front(segment);

		Segments[0]->Draw(hdc, true);
	}

	void Draw(HDC* hdc)
	{
		throw;
	}
};

std::vector<Snake*> Snakes;

void DrawSnakes(HDC* hdc)
{
	for (int i = Snakes.size() - 1; i >= 0; ++i)
	{
		Snakes[i]->Draw(hdc);
	}
}


int WINAPI WinMain(HINSTANCE hinstance, HINSTANCE hPrevInstance, LPSTR lpszCmdLine, int nCmdShow)
{
	static char szAppName[] = "Gniazdo ¿mij";
	static const int windowSize = 400;
	WNDCLASS wndClass;
	HWND hwnd;
	MSG msg;
	wndClass.style = 0;
	wndClass.lpfnWndProc = (WNDPROC)WndProc; //adres procedury obs³ugi zdarzeñ
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

	Snake* snake = new Snake();
	SnakeSegment* head = new SnakeSegment();
	head->positionX = 20;
	head->positionY = 20;
	snake->Segments.push_back(head);
	Snakes.push_back(snake);

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
	RECT rect;
	switch (message)
	{
	case WM_CREATE:
	case WM_PAINT:
		hdc = BeginPaint(hwnd, &ps);
		GetClientRect(hwnd, &rect);
		MoveGame
		EndPaint(hwnd, &ps);
		return 0;
	case WM_DESTROY:
		PostQuitMessage(0);
		return 0;
	}
	return DefWindowProc(hwnd, message, wParam, lParam);
}