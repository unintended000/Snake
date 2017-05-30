using System;
using System.Collections.Generic;
using System.IO;

namespace Snake_Class
{
    public class cField
    {
        public cField()
        {
            FieldCellList = new List<cCellField>();
            Snake = new cSnake();
        }
        
        public void Load(string sFileName) // Загрузка поля
        {
            
            string[] lines = File.ReadAllLines(sFileName);
            for (byte yy = 0; yy < lines.Length; yy++)
            {
               string[] arRows = lines[yy].Split(
               new[] { ';' }, StringSplitOptions.None);
               for (byte xx = 0; xx < arRows.Length; xx++)
                {
                    string sValue = arRows[xx];
                    switch (sValue)
                    {
                      case "0": //Ячейка травы
                             {
                                cCellField pCell = new cCellField(xx, yy);
                                pCell.Type = false;
                                FieldCellList.Add(pCell);
                                break;
                             }
                      case "1": // Ячейка стены
                            {
                               cCellField pCell = new cCellField(xx, yy);
                               pCell.Type = true;
                               FieldCellList.Add(pCell);
                               break;
                            }
                        case "2": //Змея
                            {
                                cCellSnake pCellSnake = new cCellSnake(xx, yy);
                                Snake.SnakeCellList.Add(pCellSnake);
                                cCellField pCell = new cCellField(xx, yy);
                                pCell.Type = false;
                                FieldCellList.Add(pCell);
                                break;
                            }
                        case "3": // Яблоко
                            {
                                Apple = new cApple(xx, yy); 
                                Apple.Type = type.Point;
                                cCellField pCell = new cCellField(xx, yy);
                                pCell.Type = false;
                                FieldCellList.Add(pCell);

                                break;
                            }

                        default:
                            throw new Exception("Неверный формат файла, некорректное значение " + sValue);
                            
                    }

                }
            }
        }
    
        public List<cCellField> FieldCellList 
        { get; private set; }

        public cApple Apple { get; set; }

        public cSnake Snake { get; set; }
    }
}
