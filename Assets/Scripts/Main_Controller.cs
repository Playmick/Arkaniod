using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class Main_Controller : MonoBehaviour
{
	//на платформы
	private float spdP = 0f;//скорость платформы
	private float tx1, tx0, ty1, ty0, tpx0, tpy0, tpx1, tpy1, camW, camH;
	//на шарик
    private float spdB = 0f; //скорость шара
	
	public float speedP = 30f;//скорость платформы2
	public float speedB = 30f;//скорость шара2
	
	public float x;//начальные координаты
	public float y;
	
	public float OrnX = -1;//направление по оси X
	public float OrnY = 1;//направление по оси Y
	
	public int maxScore = 20;
	
	//public GameObject pad1H;
	public GameObject pad0L;
	public GameObject ball;
	public GameObject[] sqr;
	
	int /*scrH,*/ scrL, tm;
	bool bltm, start, pause;
	
	//public Text scH;
	public Text scL;
	
	//public Text wintxtH;
	public Text wintxtL;
	
	public float sclFtr = 0.85f;//scaleFactor
	
	public GameObject GameOv;
	public GameObject btCont;
	
	int EndVect;//направление движения платформы 0 - стоит, 1 - вправо, 2 - влево
	
	void Start()
	{
		GameOv.SetActive(false);
		//время
		//bool time
		bltm = false;
		tm = 50;
		//touch ось Y/Х для платформы 1/0 
		tx1 = 0;
		tx0 = 0;
		ty1 = 0;
		ty0 = 0;
		//touch position Y/Х для платформы 1/0
		tpx0 = 0;
		tpy0 = 0;
		tpx1 = 0;
		tpy1 = 0;
		//высота экрана делённая на 2
		camH = Camera.main.pixelHeight/2f;
		//ширина экрана делённая на 2
		camW = Camera.main.pixelWidth/2f;
		
		//scrH = 0;
		scrL = 0;//обнуляем счёт
		ball.transform.position = new Vector2(x, y);//задаём начальные координаты
		//pad1H.transform.position = new Vector2(0f,250*sclFtr);
		pad0L.transform.position = new Vector2(0f,-250*sclFtr);
		
		if(!PlayerPrefs.HasKey("accel"))
		{
			PlayerPrefs.SetInt("accel", 0);
		}
		pause = false;
		EndVect = 0;
	}

    void FixedUpdate()
    {
		if (!pause)
		{
			//рестарт
			if (tm>=110 && bltm==false /*&& scrH<=19 */&& scrL<=maxScore-1)
			{
				spdB += speedB;
				spdP += speedP;
				
				bltm = true;
				tm=0;
			}
			
			//шарик
			if ((ball.transform.position.x >= 128f*sclFtr && OrnX>0)||(OrnX<0 && ball.transform.position.x <= -128f*sclFtr))//отскок от правой и левой стены
				OrnX *= -1;
			
			//отскок от верхней стены
			if (ball.transform.position.y >= 250f*sclFtr && OrnY>0)
				OrnY *= -1;
			
			//отскок от стен
			///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			//отскок от нижней платформы
			
			if(OrnY < 0 && ball.transform.position.y < pad0L.transform.position.y+14f*sclFtr && ball.transform.position.y > pad0L.transform.position.y && ball.transform.position.x < pad0L.transform.position.x + 40f*sclFtr && ball.transform.position.x > pad0L.transform.position.x - 40f*sclFtr && pad0L.activeInHierarchy)
			{
				//Debug.Log("зашёл в границы");
				if(ball.transform.position.x < pad0L.transform.position.x + 34f*sclFtr && ball.transform.position.x > pad0L.transform.position.x - 34f*sclFtr && pad0L.transform.position.x> -100f*sclFtr && pad0L.transform.position.x<100f*sclFtr)
				{
					//меняем с 45 на 70 если платформа движется против шарика по X
					if ((EndVect>0 && OrnX <-0.5f) || (EndVect<0 && OrnX >0.5f))
					{
						OrnX *= 0.36f;
						spdB *= 1.1f;
					}
					//меняем X если платформа движется против шарика летящего на 70
					else if ((EndVect>0 && OrnX == -0.36f) || (EndVect<0 && OrnX ==0.36f))
					{
						OrnX *= -1f;
						spdB *= 1.1f;
					}
				
					//меняем X если платформа движется навстречу шарику летящему на 70
					else if((EndVect>0 && OrnX == 0.36f) || (EndVect<0 && OrnX ==-0.36f))
					OrnX = (float)Math.Round((float)OrnX * 2.77f, 0, MidpointRounding.AwayFromZero);
				}
				
				//правый угол
				if(OrnX < 0 && ball.transform.position.x < pad0L.transform.position.x + 40f*sclFtr && ball.transform.position.x > pad0L.transform.position.x + 34f*sclFtr)
				{
					OrnX = 1;
					spdB*=1.1f;
				}
				//левый угол
				if(OrnX > 0 && ball.transform.position.x > pad0L.transform.position.x - 40f*sclFtr && ball.transform.position.x < pad0L.transform.position.x - 34f*sclFtr)
				{
					OrnX = -1;
					spdB*=1.1f;
				}
				//отскок по вертикали
				if(OrnY < 0 && ball.transform.position.y < pad0L.transform.position.y+14f*sclFtr && ball.transform.position.y > pad0L.transform.position.y-10*sclFtr && ball.transform.position.x < pad0L.transform.position.x + 40f*sclFtr && ball.transform.position.x > pad0L.transform.position.x - 40f*sclFtr)
				OrnY *= -1;
			}
			//отскок от платформы
			/////////////////////////////////////////////////////////////////////////////////////////////////////
			//кубик
			
			for(int i = 0; i<60; i++)
			{
				if(i!=0 && i!=1 && i!=2 && i!=3 && i!=4 && i!=5 && i!=6 && i!=7 && i!=8 && i!=9)//НЕ верхняя строка
				{
					if(i!=0 && i!=10 && i!=20 && i!=30 && i!=40 && i!=50)//НЕ первый(левый) столбец
					{
						//левый верхний угол
						if(OrnY < 0 && OrnX > 0 && ball.transform.position.y > sqr[i].transform.position.y && ball.transform.position.x < sqr[i].transform.position.x && ball.transform.position.x > sqr[i].transform.position.x - 18.5f*sclFtr && ball.transform.position.y < sqr[i].transform.position.y + 18.5f*sclFtr && sqr[i].activeInHierarchy && !sqr[i-1].activeInHierarchy && !sqr[i-10].activeInHierarchy)
						{
							//Debug.Log("1");
							OrnY *= -1;
							OrnX *= -1;
							sqr[i].SetActive(false);
						}
					}
					
					if(i!=9 && i!=19 && i!=29 && i!=39 && i!=49 && i!=59)//НЕ девятый(правый) столбец
					{
						//правый верхний
						if(OrnY < 0 && OrnX < 0 && ball.transform.position.y > sqr[i].transform.position.y && ball.transform.position.x > sqr[i].transform.position.x && ball.transform.position.x < sqr[i].transform.position.x + 18.5f*sclFtr && ball.transform.position.y < sqr[i].transform.position.y + 18.5f*sclFtr && sqr[i].activeInHierarchy && !sqr[i+1].activeInHierarchy && !sqr[i-10].activeInHierarchy)
						{
							//Debug.Log("2");
							OrnY *= -1;
							OrnX *= -1;
							sqr[i].SetActive(false);
						}
					}
					//верхняя стенка
					if(OrnY < 0 && ball.transform.position.y < sqr[i].transform.position.y+20.5f*sclFtr && ball.transform.position.y > sqr[i].transform.position.y && ball.transform.position.x > sqr[i].transform.position.x-16.5f*sclFtr && ball.transform.position.x < sqr[i].transform.position.x+16.5f*sclFtr && sqr[i].activeInHierarchy && !sqr[i-10].activeInHierarchy)
					{
						//Debug.Log("3");
						OrnY *= -1;
						sqr[i].SetActive(false);
					}	
					
				}
				
				if(i!=0 && i!=10 && i!=20 && i!=30 && i!=40 && i!=50)//НЕ первый(левый) столбец
				{
					//левая стенка
					if(OrnX > 0 && ball.transform.position.x > sqr[i].transform.position.x-20.5f*sclFtr && ball.transform.position.x < sqr[i].transform.position.x && ball.transform.position.y > sqr[i].transform.position.y-16.5f*sclFtr && ball.transform.position.y < sqr[i].transform.position.y+16.5f*sclFtr && sqr[i].activeInHierarchy && !sqr[i-1].activeInHierarchy)
					{
						//Debug.Log("5");
						OrnX *= -1;
						sqr[i].SetActive(false);
					}
					if(i==50 || i==51 || i==52 || i==53 || i==54 || i==55 || i==56 || i==57 || i==58 || i==59)
					{
						//левый нижний угол
						if(OrnY > 0 && OrnX > 0 && ball.transform.position.y < sqr[i].transform.position.y && ball.transform.position.x < sqr[i].transform.position.x && ball.transform.position.x > sqr[i].transform.position.x - 18.5f*sclFtr && ball.transform.position.y > sqr[i].transform.position.y - 18.5f*sclFtr && sqr[i].activeInHierarchy && !sqr[i-1].activeInHierarchy)
						{
							//Debug.Log("7");
							OrnY *= -1;
							OrnX *= -1;
							sqr[i].SetActive(false);
						}
					}
					else if (!sqr[i+10].activeInHierarchy)
					{
						//Debug.Log("не должно быть");
						//левый нижний угол
						if(OrnY > 0 && OrnX > 0 && ball.transform.position.y < sqr[i].transform.position.y && ball.transform.position.x < sqr[i].transform.position.x && ball.transform.position.x > sqr[i].transform.position.x - 18.5f*sclFtr && ball.transform.position.y > sqr[i].transform.position.y - 18.5f*sclFtr && sqr[i].activeInHierarchy && !sqr[i-1].activeInHierarchy)
						{
							//Debug.Log("9");
							OrnY *= -1;
							OrnX *= -1;
							sqr[i].SetActive(false);
						}
					}
				}
				
				if(i!=9 && i!=19 && i!=29 && i!=39 && i!=49 && i!=59)//НЕ девятый(правый) столбец
				{
					if(i==50 || i==51 || i==52 || i==53 || i==54 || i==55 || i==56 || i==57 || i==58 || i==59)
					{
						//правый нижний
						if(OrnY > 0 && OrnX < 0 && ball.transform.position.y < sqr[i].transform.position.y && ball.transform.position.x > sqr[i].transform.position.x && ball.transform.position.x < sqr[i].transform.position.x + 18.5f*sclFtr && ball.transform.position.y > sqr[i].transform.position.y - 18.5f*sclFtr && sqr[i].activeInHierarchy && !sqr[i+1].activeInHierarchy)
						{
							OrnY *= -1;
							OrnX *= -1;
							sqr[i].SetActive(false);
						}
					}
					else if (!sqr[i+10].activeInHierarchy)
					{
						//правый нижний
						if(OrnY > 0 && OrnX < 0 && ball.transform.position.y < sqr[i].transform.position.y && ball.transform.position.x > sqr[i].transform.position.x && ball.transform.position.x < sqr[i].transform.position.x + 18.5f*sclFtr && ball.transform.position.y > sqr[i].transform.position.y - 18.5f*sclFtr && sqr[i].activeInHierarchy && !sqr[i+1].activeInHierarchy)
						{
							OrnY *= -1;
							OrnX *= -1;
							sqr[i].SetActive(false);
						}
					}
					//правая стенка
					if(OrnX < 0 && ball.transform.position.x < sqr[i].transform.position.x+20.5f*sclFtr && ball.transform.position.x > sqr[i].transform.position.x && ball.transform.position.y > sqr[i].transform.position.y-16.5f*sclFtr && ball.transform.position.y < sqr[i].transform.position.y+16.5f*sclFtr && sqr[i].activeInHierarchy && !sqr[i+1].activeInHierarchy)
					{
						OrnX *= -1;
						sqr[i].SetActive(false);
					}
					
				}
				
				//если нижняя строка то не проверять на наличие снизу куба
				if(i==50 || i==51 || i==52 || i==53 || i==54 || i==55 || i==56 || i==57 || i==58 || i==59)
				{
					//нижняя стенка
					if(OrnY > 0 && ball.transform.position.y > sqr[i].transform.position.y-20.5f*sclFtr && ball.transform.position.y < sqr[i].transform.position.y && ball.transform.position.x > sqr[i].transform.position.x-16.5f*sclFtr && ball.transform.position.x < sqr[i].transform.position.x+16.5f*sclFtr && sqr[i].activeInHierarchy)
					{
						OrnY *= -1;
						sqr[i].SetActive(false);
					}
				}
				else if (!sqr[i+10].activeInHierarchy)
				{
					//нижняя стенка
					if(OrnY > 0 && ball.transform.position.y > sqr[i].transform.position.y-20.5f*sclFtr && ball.transform.position.y < sqr[i].transform.position.y && ball.transform.position.x > sqr[i].transform.position.x-16.5f*sclFtr && ball.transform.position.x < sqr[i].transform.position.x+16.5f*sclFtr && sqr[i].activeInHierarchy)
					{
						OrnY *= -1;
						sqr[i].SetActive(false);
					}
				}
			}
			
			int e=0;
			
			for(int i = 0; i<60;i++)
			{
				
				if (sqr[i].activeInHierarchy == false)
					e++;
				if (e>=60)
				{
					btCont.SetActive(false);
					spdB = 0f;
					spdP = 0f;
					wintxtL.text = "Вы выиграли!";
					GameOv.SetActive(true);
					PlayerPrefs.SetInt("curlvl", SceneManager.GetActiveScene().buildIndex+0);
				}
			}
			
			if ((ball.transform.position.y < pad0L.transform.position.y -(1-sclFtr)*250 && OrnY<0)) Stop(); // если вылетел за пределы, запускаем game over
			
			ball.transform.Translate(Vector2.up * spdB * 0.1f * OrnY);//передвигаем по оси Y
			ball.transform.Translate(Vector2.right * spdB * 0.1f * OrnX);//передвигаем по оси Х
			
			//физика шарика
			///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			tm+=1;
			if (tm>=1000)
			{
				spdB*=1.1f;
				tm=0;
			}
			/////////////////////////////////////////////////
			if (PlayerPrefs.GetInt("accel")==0)//если акселерометр выключен
			{
				if (Input.touchCount>0)
				{
					//инициализация первого пальца
					Touch p0 = Input.GetTouch(0);
					tpx0 = ((p0.position.x - camW)/(((camW)-(camW/4f))/100f));
					
					//первый палец на экране
					tx0 = tpx0;
				}
			}
			//обработка нажатий
			////////////////////////////////////////////////////////////////////////////////////////////////////
			//платформа
			if (PlayerPrefs.GetInt("accel")==0)//если акселерометр выключен
			{
				if (tx0 + 3f>pad0L.transform.position.x && tx0 - 3f<pad0L.transform.position.x)
				{
					EndVect = 0;
					pad0L.transform.Translate(Vector2.right * 0);
				}
				//вправо нижняя платформа
				else if((tx0 + 3f>=pad0L.transform.position.x)&&(pad0L.transform.position.x < 102f*sclFtr))
				{
					EndVect = 1;
					pad0L.transform.Translate(Vector2.right * spdP * 0.1f);
				}
				//влево нижняя платформа
				else if((tx0 - 3f<=pad0L.transform.position.x)&&(pad0L.transform.position.x > -102f*sclFtr))
				{
					EndVect = -1;
					pad0L.transform.Translate(-Vector2.right * spdP * 0.1f);
				}
			}else if (PlayerPrefs.GetInt("accel")==1)//если акселерометр включен
			{
				//вправо
				if (pad0L.transform.position.x < 102f*sclFtr && Input.acceleration.x>0.05f)
				{
					EndVect = 1;
					if (Input.acceleration.x*2<=1)
						pad0L.transform.Translate(Vector2.right * spdP * Input.acceleration.x*2 *0.1f);
					else
						pad0L.transform.Translate(Vector2.right * spdP *0.1f);
				}
				//влево
				else if ((pad0L.transform.position.x > -102f*sclFtr)&& Input.acceleration.x<-0.05f)
				{
					EndVect = -1;
					if (Input.acceleration.x*2>=-1)
						pad0L.transform.Translate(Vector2.right * spdP * Input.acceleration.x*2 *0.1f);
					else
						pad0L.transform.Translate(-Vector2.right * spdP *0.1f);
				}
			}
			if(Input.GetKey(KeyCode.Home) || Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Menu))
			{
				pause = true;
			}
		}
		else if (pause)
		{
			GameOv.SetActive(true);
			btCont.SetActive(true);
		}
    }
	//платформы
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	
	void Stop()
	{
		spdB = 0f;
		spdP = 0f;
		wintxtL.text = "Вы проиграли!";
		GameOv.SetActive(true);
		btCont.SetActive(false);
	}
	
	public void OnClickMM()
	{
		SceneManager.LoadScene("lvlSel");
	}
	
	public void OnClickRestart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+0);
	}
	public void OnClickCont()
	{
		pause = false;
		GameOv.SetActive(false);
		btCont.SetActive(false);
	}
}
