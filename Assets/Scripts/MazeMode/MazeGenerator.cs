using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {

	int rows;
	int cols;

	public GameObject wall;
	public GameObject player;
	public GameObject exit;
	public Vector3 offset;
	public GameObject flashlight;
	public GameObject coin;
	int[,] board;
	int [] xy;
	void Start () {
		offset = new Vector3 (8F, 18F, -11F);

		prepBoard (10, 10);
		makeMaze ();
		xy = getRandomEmptyPosition ();
		player = Instantiate(player,new Vector3(xy[0]-11,0.5F,xy[1]-11),Quaternion.identity);
		Camera.main.transform.position = player.transform.position + offset;
		Camera.main.transform.SetParent (player.transform);

		for (int i = 1; i <= Random.Range (7, 10); i++) {
		
			xy = getRandomEmptyPosition ();
			coin = Instantiate (coin, new Vector3 (xy [0] - 11.5F, 0F, xy [1] - 11), Quaternion.Euler(new Vector3(90,0,0)));

		}

		flashlight = Instantiate(flashlight,new Vector3(0,0,0),Quaternion.identity);

		flashlight.transform.position = player.transform.position + new Vector3 (0, 1.8F, -0.2F);
		flashlight.transform.SetParent (player.transform);

		int entrance_row = Random.Range(0,8) * 2 +1;     
		if(!(xy[1]<=11))
		{
			board[entrance_row,0] = 4;

		}
		else
		{
			board[entrance_row,getCols()-1] = 4;

		} 

		drawMaze (10,10);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void prepBoard(int row,int col)
	{

		//row,col = nr randuri care pot fi parcurse
		// row*2+1 nr de randuri necesare in matrice pt a avea un lavirint cu row culoare
		rows = row*2+1;
		cols = col*2+1;	

		//crearea matricei si bordarea ei cu pereti
		board = new int[rows*3,cols*3];
		for(int i=0; i<rows; i++){
			board[i,0] = 1;
			board[i,cols-1] = 1;
		}

		for(int i=0; i<cols; i++){
			board[0,i] = 1;
			board[rows-1,i] = 1;
		}
	}

	public void makeMaze()
	{
		makeMaze(0,cols-1,0,rows-1);
	}

	//pozitii pare=ziduri
	//pozitii impare=drumuri

	private void makeMaze(int l, int r, int t, int b)
	{
		//calculeaza dimensiunile zonei curente
		int width = r-l;
		int height = b-t;
		//impartire in functie de dimensiunile zonei
		if(width > 2 && height > 2){

			if(width > height)
				divideVertical(l, r, t, b);

			else if(height > width)
				divideHorizontal(l, r, t, b);

			else if(height == width){
				Random rand = new Random();
				int pickOne = Random.Range(0,1);

				if(pickOne==1)
					divideVertical(l, r, t, b);
				else
					divideHorizontal(l, r, t, b);
			}
		}else if(width > 2 && height <=2){
			divideVertical(l, r, t, b);
		}else if(width <=2 && height > 2){
			divideHorizontal(l, r, t, b);
		}
	}


	private void divideVertical(int l, int r, int t, int b)
	{
		//impartire pe verticala

		//alege coloana. zidurile se vor genera doar pe pozitii pare

		int divide =  l + 2 + Random.Range(0,(r-l-3));

		if (divide % 2 == 1)
			divide++;
		//genereaza zid de sus pana jos la coloana divide
		for(int i=t; i<b; i++){
			board[i,divide] = 1;
		}

		//crearea unui spatiu de trecere in peretele generat, pentru a permite trecerea
		//dintr-o zona a labirintului in cealalta
		//astfel, nu va exista nicio zona inaccesibila in labirint
		//spatiile se afla mereu pe pozitii impare

		int clearSpace = t + Random.Range(0, (b-t -2));
		if (clearSpace % 2 == 0)
			clearSpace++;

		board[clearSpace,divide] = 0;     
		//reapeleaza functia de impartire pe cele doua zone noi create
		makeMaze(l, divide, t, b);
		makeMaze(divide, r, t, b);
	}


	private void divideHorizontal(int l, int r, int t, int b)
	{
		//impartire pe orizontala

		//alege rand
		int divide =  t + 2 + Random.Range(0,(b-t-3));
		if(divide%2 == 1)
			divide++;

		for(int i=l; i<r; i++){
			board[divide,i] = 1;
		}
		//crearea unui spatiu de trecere in peretele generat, pentru a permite trecerea dintr-o zona a labirintului in cealalta
		//astfel, nu va exista nicio zona inaccesibila in labirint
		//spatiile libere trebuie sa fie pe pozitii impare(ca sa se conecteze cu pasajele dintre ziduri)
		int clearSpace = l + Random.Range(0,(r-l -2));
		if (clearSpace % 2 == 0)
			clearSpace++;
		//reapeleaza functia de impartire pe cele doua zone noi create
		board[divide,clearSpace] = 0;
		makeMaze(l, r, t, divide);
		makeMaze(l, r, divide, b);
	}

	public int[,] getMaze(){
		return board;
	}
	public int getRows() {
		return rows;
	}
	public int getCols() {
		return cols;
	}
	public int[] getRandomEmptyPosition()
	{
		int x=0,y=0;
		int[] xy = new int[2];
		x=Random.Range(0,getRows()-2);
		y=Random.Range(0,getCols()-2);        
		while(board[x,y]!=0)
		{
			x=Random.Range(0,getRows()-1);
			y=Random.Range(0,getCols()-1);
		}
		board[x,y]=3;
		xy[0]=x;
		xy[1]=y;
		return xy;
	}


	private void drawMaze(int row, int col)
	{
		for(int i=0;i<=getRows()-1;i++)
			for(int j=0;j<=getCols()-1;j++)
			{
				if(board[i,j]==1)
				{
					//11.5,11 sunt offsetul de la centrul hartii.
					wall = Instantiate (wall, new Vector3 (i-11.5F, 0.5F, j-11), Quaternion.identity);
				}
				else if (board[i,j]==4)
					exit = Instantiate(exit,  new Vector3 (i-11.5F, 0.5F, j-11), Quaternion.identity);
			}        
	}
}
