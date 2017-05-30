using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Snake_Class
{
   public class cGame
    {  
        #region Свойства
        public int Points { get; set; }
        public byte Life  { get; set; }
        public cField Field { get; set; }
        public String FieldName { get; set; }
        private Timer m_pGameTimer;
        private EDirection m_enDirection;
        public bool check;

        
        #endregion

        #region Game
        /// <summary>
        /// Создание поля
        /// </summary>
        /// <param name="sFileName"></param>
        public void CreateField(string sFileName) 
        {
            Field = new cField();
            Field.Load(sFileName);
            FieldName = sFileName;
        }
        public void Pause()
        {
            m_pGameTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }
        public void Play()
        {

            TimeSpan tsStart = new TimeSpan(0, 0, 0, 0, 250);
            TimeSpan tsPeriod = new TimeSpan(0, 0, 0, 0, 250);
            m_pGameTimer.Change(tsStart, tsPeriod);
        }
       
        /// <summary>
        /// Новая игра
        /// </summary>
        public void NewGame()
        {
            Life = 3;
            Points = 0;
            m_pGameTimer = new Timer(h_Tick);
            
        }
       
        /// <summary>
        /// Старт игры
        /// </summary>
        public void Start()
        {
            TimeSpan tsStart = new TimeSpan(0, 0, 0, 0, 250);
            TimeSpan tsPeriod = new TimeSpan(0, 0, 0, 0, 250);
            m_pGameTimer.Change(tsStart, tsPeriod);
            check = false;
        }
     
        /// <summary>
        /// Конце игры
        /// </summary>
        public void EndGame()
        {
            m_pGameTimer.Change(Timeout.Infinite, Timeout.Infinite);
            check = true;
        }
        #endregion

        #region Direction 
        public EDirection Direction
        {
          get { return m_enDirection; }
        }

        public enum EDirection
        {
            Up,
            Down,
            Left,
            Right
        }

        public void Turn(EDirection enDirection)
        {
            m_enDirection = enDirection;
        }

        private bool Check(cCellSnake CellSnake)
        {
            cCellField CellField = Field.FieldCellList.FirstOrDefault(p => (p.CoordX == CellSnake.CoordX) && (p.CoordY == CellSnake.CoordY));
            if (CellField.Type == true)
                return false;//Проверка на бортик
            else
                return true;


        }
        #endregion
        private void h_Tick(object state)
        {
            if (Life != 0)
            {
                cCellSnake CellSnake = new cCellSnake(Field.Snake.SnakeCellList.First().CoordX, Field.Snake.SnakeCellList.First().CoordY);

                switch (m_enDirection)
                {
                    case EDirection.Left: 
                        CellSnake.CoordX--;
                        break;
                    case EDirection.Right:
                        CellSnake.CoordX++;
                        break;
                    case EDirection.Up:
                        CellSnake.CoordY--;
                        break;
                    case EDirection.Down:
                        CellSnake.CoordY++;
                        break;
                }

                if (Check(CellSnake) == true)
                {
                    //Проверка на яблоко
                    if ((CellSnake.CoordX == Field.Apple.CoordX) && (CellSnake.CoordY == Field.Apple.CoordY))
                     AppleWork(CellSnake);
                    
                    else //"Движение" змеи
                    {
                        Field.Snake.SnakeCellList.Insert(0, CellSnake);
                        Field.Snake.SnakeCellList.RemoveAt(Field.Snake.SnakeCellList.Count - 1);
                    }
                }

                else //Если бортик
                {
                    EndGame();
                    Life--;
                    CreateField(FieldName); //Возвращаем первоначальный вид змеи
                    Start();
                }
            }
            else  EndGame();  //Конец игры

        }

        #region   Яблоко
        
        /// <summary>
        /// Работа яблока
        /// </summary>
        /// <param name="CellSnake"></param>
        private void AppleWork(cCellSnake CellSnake)
        {
            Field.Snake.SnakeCellList.Insert(0, CellSnake);
            switch (Field.Apple.Type)
            {
                case type.Point: //Очки +10
                    Points = Points + 10;
                    break;
                case type.BigPoint: //Очки +50
                    Points = Points + 50;
                    break;
                case type.Life: //Очки +10 жизнь +1
                    Points = Points + 10;
                        if (Life < 4)
                        Life++;
                    break;
                case type.Surprise: //Яблоко сюрприз
                    {
                        Random rand = new Random();
                        int N = rand.Next(0, 100);

                        if (N < 60)                  //Очки +50
                            Points = Points + 50;
                        else if ((N > 60) & (N < 70))//Очки -10
                            Points = Points - 10;

                        else if ((N > 70) & (N < 80))//Очки +10 жизнь +1
                        {
                            Points = Points + 10;
                            if (Life < 4)
                               Life++;
                        }

                        else if ((N > 80) & (N < 96)) // Отпадает хвост
                        {
                            if (Field.Snake.SnakeCellList.Count > 5) // Для длинной змеи
                            {
                               Random r = new Random();
                               int m = r.Next(1, 5);
                               for (int i = 0; i <= m; i++)
                               {
                                  Field.Snake.SnakeCellList.RemoveAt(Field.Snake.SnakeCellList.Count - 1);
                               }
                            }
                            else if (Field.Snake.SnakeCellList.Count >= 3) // Для короткой змеи
                            {
                               Random r = new Random();
                               int m = r.Next(1, 3);
                               for (int i = 0; i <= m; i++)
                               {
                                  Field.Snake.SnakeCellList.RemoveAt(Field.Snake.SnakeCellList.Count - 1);
                               }
                            }
                        }
                        else Life--; // Жизнь -1
                        break;
                   }
            }
           NewApple();
        }  
        
        /// <summary>
        /// Новое яблоко
        /// </summary>
        private void NewApple() 
        {
            Random rand = new Random();
            int coord = rand.Next(0, Field.FieldCellList.Count);
            cCellField pField = Field.FieldCellList[coord];
            cCellSnake pSnake = Field.Snake.SnakeCellList.FirstOrDefault(p => (p.CoordX == pField.CoordX) && (p.CoordY == pField.CoordY));

            do
            {
                coord = rand.Next(0, Field.FieldCellList.Count);  //Берем рандомный элемент листа
                pField = Field.FieldCellList[coord];
                pSnake = Field.Snake.SnakeCellList.FirstOrDefault(p => (p.CoordX == pField.CoordX) && (p.CoordY == pField.CoordY)); //Проверяем координаты
            }
            while ((pField.Type == true) || (pSnake != null)); //Проверка на бортик и змею
            
            Field.Apple.CoordX = pField.CoordX;
            Field.Apple.CoordY = pField.CoordY;
            AppleType(Field.Apple);

        }
       
        /// <summary>
        /// Новый тип яблока
        /// </summary>
        /// <param name="Apple_old"></param>
        /// <returns></returns>
        private cApple AppleType(cApple Apple_old)  
        {
            cApple Apple = Apple_old;
            Random rand = new Random();
             int N = rand.Next(0, 100);

            if (N < 60)
               Apple.Type = type.Point;
            else if ((N > 60) & (N < 80))
               Apple.Type = type.BigPoint;
            else if ((N > 80) & (N < 96))
               Apple.Type = type.Surprise;
            else Apple.Type = type.Life;
            return Apple;

        }
        #endregion
       
              
       
    }
}
