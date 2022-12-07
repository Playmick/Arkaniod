using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class Pong_Main_Controller : MonoBehaviour
{
	//на платформы
	private float spdP = 0f;//скорость платформы
	private float tx1, tx0, ty1, ty0, tpx0, tpy0, tpx1, tpy1, camW, camH;
	//на шарик
    private float spdB = 0f; //скорость шара
	
	public float speedP = 30f;//скорость платформы2
	public float speedB = 30f;//скорость шара2
	
	
	
	public float OrnX = -1;//направление по оси X
	public float OrnY = 1;//направление по оси Y
	
	public  int maxScore = 20;
	
	public float x;
	public float y;
	public GameObject pad1H;
	public GameObject pad0L;
	public GameObject ball;
	public GameObject btRest;
	public GameObject btMM;
	//public GameObject/*[]*/ sqr;
	
	int scrH, scrL, tm;
	bool bltm, start, pause;
	
	public Text scH;
	public Text scL;
	
	public Text wintxtH;
	public Text wintxtL;
	
	public float sclFtr = 0.85f;//scaleFactor
	
	public GameObject btCont;
	int EndVect0;
	int EndVect1;
	
	void Start()
	{
		btRest.SetActive(false);
		btMM.SetActive(false);
		btCont.SetActive(false);
		//время
		//bool time
		bltm = false;
		tm = 50;
		//touch ось Y/Х для платформы 1/0 
		tx1 = pad1H.transform.position.x;
		ty1 = 1;
		tx0 = pad0L.transform.position.x;
		ty0 = -1;
		//touch position Y/Х для платформы 1/0
		tpx0 = 0;
		tpy0 = 0;
		tpx1 = 0;
		tpy1 = 0;
		//высота экрана делённая на 2
		camH = Camera.main.pixelHeight/2f;
		//ширина экрана делённая на 2
		camW = Camera.main.pixelWidth/2f;
		
		scrH = 0;//обнуляем счёт
		scrL = 0;
		ball.transform.position = new Vector2(x, y);//задаём начальные координаты???
		pad1H.transform.position = new Vector2(0f,250*sclFtr);
		pad0L.transform.position = new Vector2(0f,-250*sclFtr);
		int EndVect0=0;
		int EndVect1=0;
	}

    void FixedUpdate()
    {
		if(!pause)
		{
			//рестарт
			if (tm>=110 && bltm==false && scrH<=19 && scrL<=19)
			{
				spdB += speedB;
				spdP += speedP;
				
				bltm = true;
				tm=0;
			}
			
			if (Input.touchCount>0)
			{
				//инициализация первого пальца
				Touch p0 = Input.GetTouch(0);
				tpx0 = ((p0.position.x - camW)/(((camW)-(camW/4f))/100f));
				tpy0 = (p0.position.y - camH);
				
				if (tpy0<0)
				{
					//первый палец в нижней части экрана
					tx0 = tpx0;
				}
				else if (tpy0>0)
					{
						//первый палец в верхней части экрана
						tx1 = tpx0;
					}
				
				
				if (Input.touchCount>1)
				{
					//инициализация второго пальца
					Touch p1 = Input.GetTouch(1);
					tpx1 = ((p1.position.x - camW)/(((camW)-(camW/4f))/100f));
					tpy1 = (p1.position.y - camH);
					
					if (tpy1<0)
					{
						//второй палец в нижней части экрана
						tx0 = tpx1;
					}
					else if (tpy1>0)
					{
						//второй палец в верхней части экрана
						tx1 = tpx1;
					}
					
					if ( tpy1<0 && tpy0<0 )
						tx0 = pad0L.transform.position.x;
					
					if ( tpy1>0 && tpy0>0 )
						tx1 = pad1H.transform.position.x;
					
				}
			}
			
			//обработка нажатий
			////////////////////////////////////////////////////////////////////////////////////////////////////
			//шарик
			
			
			
			if ((ball.transform.position.x >= 128f*sclFtr && OrnX>0)||(OrnX<0 && ball.transform.position.x <= -128f*sclFtr))//отскок от правой и левой стены
				OrnX *= -1;
			
			//верхняя платформа
			if(OrnY > 0 && ball.transform.position.y > pad1H.transform.position.y-14f*sclFtr && ball.transform.position.y < pad1H.transform.position.y && ball.transform.position.x < pad1H.transform.position.x + 40f*sclFtr && ball.transform.position.x > pad1H.transform.position.x - 40f*sclFtr)
			{
				//Debug.Log("зашёл в границы");
				if(ball.transform.position.x < pad1H.transform.position.x + 34f*sclFtr && ball.transform.position.x > pad1H.transform.position.x - 34f*sclFtr && pad1H.transform.position.x> -100f*sclFtr && pad1H.transform.position.x<100f*sclFtr)
				{
					//меняем с 45 на 70 если платформа движется против шарика по X
					if ( (EndVect1>0 && OrnX <-0.5f) || (EndVect1<0 && OrnX >0.5f))
					{
						OrnX *= 0.36f;
						spdB *= 1.1f;
					}
				
					//меняем X если платформа движется против шарика летящего на 70
					else if ((EndVect1>0 && OrnX == -0.36f) || (EndVect1<0 && OrnX ==0.36f))
					{
						OrnX *= -1f;
						spdB *= 1.1f;
					}
				
					//меняем X если платформа движется навстречу шарику летящему на 70
					else if((EndVect1>0 && OrnX == 0.36f) || (EndVect1<0 && OrnX ==-0.36f))
					OrnX = (float)Math.Round((float)OrnX * 2.77f, 0, MidpointRounding.AwayFromZero);
				}	
				
				//правый угол
				if(OrnX < 0 && ball.transform.position.x < pad1H.transform.position.x + 40f*sclFtr && ball.transform.position.x > pad1H.transform.position.x + 34f*sclFtr)
					{
						spdB*=1.1f;
						OrnX = 1;
					}
				
				//левый угол
				if(OrnX > 0 && ball.transform.position.x > pad1H.transform.position.x - 40f*sclFtr && ball.transform.position.x < pad1H.transform.position.x - 34f*sclFtr)
					{
						spdB*=1.1f;
						OrnX = -1;
					}
				//отскок по вертикали
				if(OrnY > 0 && ball.transform.position.y > pad1H.transform.position.y-14f*sclFtr && ball.transform.position.y < pad1H.transform.position.y+10*sclFtr && ball.transform.position.x < pad1H.transform.position.x + 40f*sclFtr && ball.transform.position.x > pad1H.transform.position.x - 40f*sclFtr)
					OrnY *= -1;
			}
			
			//отскок от верхней платформы
			///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			//отскок от нижней платформы
			
			if(OrnY < 0 && ball.transform.position.y < pad0L.transform.position.y+14f*sclFtr && ball.transform.position.y > pad0L.transform.position.y && ball.transform.position.x < pad0L.transform.position.x + 40f*sclFtr && ball.transform.position.x > pad0L.transform.position.x - 40f*sclFtr)
			{
				//Debug.Log("зашёл в границы");
				if(ball.transform.position.x < pad0L.transform.position.x + 34f*sclFtr && ball.transform.position.x > pad0L.transform.position.x - 34f*sclFtr && pad0L.transform.position.x> -100f*sclFtr && pad0L.transform.position.x<100f*sclFtr)
				{
					//меняем с 45 на 70 если платформа движется против шарика по X
					if ( (EndVect0>0 && OrnX <-0.5f) || (EndVect0<0 && OrnX >0.5f))
					{
						OrnX *= 0.36f;
						spdB *= 1.1f;
					}
					//меняем X если платформа движется против шарика летящего на 70
					else if ((EndVect0>0 && OrnX == -0.36f) || (EndVect0<0 && OrnX ==0.36f))
					{
						OrnX *= -1f;
						spdB *= 1.1f;
					}
				
					//меняем X если платформа движется навстречу шарику летящему на 70
					else if((EndVect0>0 && OrnX == 0.36f) || (EndVect0<0 && OrnX ==-0.36f))
					OrnX = (float)Math.Round((float)OrnX * 2.77f, 0, MidpointRounding.AwayFromZero);
				}	
				
				//правый угол
				if(OrnX < 0 && ball.transform.position.x < pad0L.transform.position.x + 40f*sclFtr && ball.transform.position.x > pad0L.transform.position.x + 34f*sclFtr)
					{
						spdB*=1.1f;
						OrnX = 1;
					}
				
				//левый угол
				if(OrnX > 0 && ball.transform.position.x > pad0L.transform.position.x - 40f*sclFtr && ball.transform.position.x < pad0L.transform.position.x - 34f*sclFtr)
					{
						spdB*=1.1f;
						OrnX = -1;
					}
				//отскок по вертикали
				if(OrnY < 0 && ball.transform.position.y < pad0L.transform.position.y+14f*sclFtr && ball.transform.position.y > pad0L.transform.position.y-10*sclFtr && ball.transform.position.x < pad0L.transform.position.x + 40f*sclFtr && ball.transform.position.x > pad0L.transform.position.x - 40f*sclFtr)
				OrnY *= -1;
			}
			
			//физика шарика
			///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			
			else if ((ball.transform.position.y > pad1H.transform.position.y +(1-sclFtr)*250 && OrnY>0) || (ball.transform.position.y < pad0L.transform.position.y -(1-sclFtr)*250 && OrnY<0)) Stop(); // если вылетел за пределы, запускаем game over
			
			ball.transform.Translate(Vector2.up * spdB * 0.1f * OrnY);//передвигаем по оси Y
			ball.transform.Translate(Vector2.right * spdB * 0.1f * OrnX);//передвигаем по оси Х
			
			tm+=1;
			if (tm>=1000)
			{
				spdB*=1.1f;
				tm=0;
			}
			
			//движение платформ
			//вправо верхняя платформа
			if (tx1 + 3f>pad1H.transform.position.x && tx1 - 3f<pad1H.transform.position.x)
			{
				EndVect1 = 0;
				pad1H.transform.Translate(Vector2.right * 0);
			}
			//вправо нижняя платформа
			else if((tx1 + 3f>=pad1H.transform.position.x)&&(pad1H.transform.position.x < 102f*sclFtr))
			{
				EndVect1 = 1;
				pad1H.transform.Translate(Vector2.right * spdP * 0.1f);
			}
			//влево нижняя платформа
			else if((tx1 - 3f<=pad1H.transform.position.x)&&(pad1H.transform.position.x > -102f*sclFtr))
			{
				EndVect1 = -1;
				pad1H.transform.Translate(-Vector2.right * spdP * 0.1f);
			}
			
			//верхняя платформа
			///////////////////////////////////////////////////////////////////////////////////////////////////
			//нижняя платформа
			
			if (tx0 + 3f>pad0L.transform.position.x && tx0 - 3f<pad0L.transform.position.x)
			{
				EndVect0 = 0;
				pad0L.transform.Translate(Vector2.right * 0);
			}
			//вправо нижняя платформа
			else if((tx0 + 3f>=pad0L.transform.position.x)&&(pad0L.transform.position.x < 102f*sclFtr))
			{
				EndVect0 = 1;
				pad0L.transform.Translate(Vector2.right * spdP * 0.1f);
			}
			//влево нижняя платформа
			else if((tx0 - 3f<=pad0L.transform.position.x)&&(pad0L.transform.position.x > -102f*sclFtr))
			{
				EndVect0 = -1;
				pad0L.transform.Translate(-Vector2.right * spdP * 0.1f);
			}
			//платформы
			//////////////////////////////////////////////////////////////////////////////////////////////////////
			
			
			if (scrL>maxScore-1)
			{
				wintxtL.text = "You won!";
				wintxtH.text = "You're a loser!";
				spdB = 0f;
				spdP = 0f;
				btRest.SetActive(true);
				btMM.SetActive(true);
				/*if (tm>=250)
				{
					SceneManager.LoadScene(0);
					/*Application.Quit();
					Time.timeScale = 0;
				}*/
			}
			else if (scrH>maxScore-1)
			{
				wintxtH.text = "You won!";
				wintxtL.text = "You're a loser!";
				spdB = 0f;
				spdP = 0f;
				btRest.SetActive(true);
				btMM.SetActive(true);
				/*if (tm>=250)
				{
					SceneManager.LoadScene(0);
					/*Application.Quit();
					Time.timeScale = 0;
				}*/
			}
			if(Input.GetKey(KeyCode.Home) || Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Menu))
			{
				pause = true;
			}
		}
		else if (pause)
		{
			btRest.SetActive(true);
			btMM.SetActive(true);
			btCont.SetActive(true);
		}
    }
	
	void Stop()
	{
		//останавливаем платформы
		tx1 = pad1H.transform.position.x;
		tx0 = pad0L.transform.position.x;
		int EndVect0=0;
		int EndVect1=0;
		/*transform.Translate(Vector2.up*0f);
		transform.Translate(Vector2.right*0f);*/
		if (bltm)
		{
			tm = 0;
			bltm = false;
		}
		spdB = 0f;
		spdP = 0f;
		if ((ball.transform.position.y > pad1H.transform.position.y +(1-sclFtr)*250 && OrnY>0) && tm==0f && (btRest.activeInHierarchy==false)) 
		{
			if (scrL<99)
			scrL +=1;
			scL.text = "" + scrL;
		}
		else if ((ball.transform.position.y < pad0L.transform.position.y -(1-sclFtr)*250 && OrnY<0) && tm==0f && (btRest.activeInHierarchy==false)) 
		{
			if (scrH<99)
			scrH +=1;
			scH.text = "" + scrH;
		}
		if (tm>=75 && scrH<maxScore && scrL<maxScore)
		{
			ball.transform.position = new Vector2();
		}
		//ball.transform.position = new Vector2();
		//Time.timeScale = 0;//нужно сделать ручную остановку через 2 переменные
		//Application.Quit();
	}
	public void OnClickMM()
	{
		SceneManager.LoadScene("GamSel");
	}
	
	public void OnClickCont()
	{
		btRest.SetActive(false);
		btMM.SetActive(false);
		btCont.SetActive(false);
		pause = false;
	}
	
	public void OnClickRestart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+0);
	}
	
}
