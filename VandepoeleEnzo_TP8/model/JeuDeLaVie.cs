using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace VandepoeleEnzo_TP8.model
{
    public class JeuDeLaVie
    {
        /// <summary>
        /// Property permettant de lire le nombre de lignes
        /// </summary>
        public int nbRow { get { return _nbRow; } }

        /// <summary>
        /// Property permettant de lire le nombre de colonnes
        /// </summary>
        public int nbCol { get { return _nbCol; } }

        /// <summary>
        /// Property permettant de lire (public) et écrire (private) le nombre courant de générations
        /// </summary>
        public int pCptGeneration { get { return _cptGeneration; } private set { _cptGeneration = value;} }

        /// <summary>
        /// Constructeur de la classe. Ne prends pas de paramètres en entrée
        /// </summary>
        public JeuDeLaVie()
        {
            for (int idRow = 0; idRow < 50; idRow++)
            {
                List<bool> listRow = new List<bool>();
                _cellState.Add(listRow);
                List<bool> listRowNextGenertion = new List<bool>();
                _cellStateNextGeneration.Add(listRowNextGenertion);

                for (int idCol = 0; idCol < 50; idCol++)
                {
                    _cellState[idRow].Add(false);
                    _cellStateNextGeneration[idRow].Add(false);
                }
            }
        }

        /// <summary>
        /// Permet une remise à zéro de l'état des cellule et du compteur de générations
        /// </summary>
        public void reset()
        {
            pCptGeneration = 0;

            for (int idRow = 0; idRow < 50; idRow++)
            {
                for (int idCol = 0; idCol < 50; idCol++)
                {
                    _cellState[idRow][idCol] = false;
                    _cellStateNextGeneration[idRow][idCol] = false;
                }
            }
        }

        /// <summary>
        /// Permet de changer l'état d'une cellule de l'automate
        /// </summary>
        /// <param name="idRow">l'id de la ligne de la cellule à changer</param>
        /// <param name="idCol">l'id de la colonne de la cellule à changer</param>
        /// <returns> le nouvel état de la cellule</returns>
        public bool changeCellState(int idRow, int idCol)
        {

            _cellState[idRow][idCol] = !_cellState[idRow][idCol];

            return _cellState[idRow][idCol];
        }

        /// <summary>
        /// Calcul le nouvel état de toutes les cellules, incrémente le compteur de generation
        /// </summary>
        /// <returns>La liste des Point(idRow, idCol) des cellules vivantes</returns>
        public List<Point> getListAliveCellNextGeneration()
        {
            computeGeneration();
            return getListAliveCell();
        }

        /**********************************************PRIVATE******************************************************/

        List<List<bool>> _cellStateNextGeneration = new List<List<bool>>();
        List< List <bool> > _cellState = new List< List <bool > >();

        List<List<bool>> _cellStateTmp;

        int _cptGeneration = 0;
        

        int _nbRow = 50;
        int _nbCol = 50;


        private void computeGeneration()
        {
            for (int idRow = 0; idRow < 50; idRow++)
            {
                for (int idCol = 0; idCol < 50; idCol++)
                {
                    _cellStateNextGeneration[idRow][idCol] = false;

                    int nbAliveNeighbour = 0;
                    bool currCellIsAlive = _cellState[idRow][idCol];

                    for (int iSubRow = idRow - 1; iSubRow < idRow + 2; iSubRow++)
                    {
                        for (int idSubCol = idCol-1; idSubCol < idCol + 2; idSubCol++)
                        {
                            if ((iSubRow >= 0) && (iSubRow < _nbRow) && (idSubCol >= 0) && (idSubCol < _nbCol))
                            {
                                if ((iSubRow != idRow) || (idSubCol != idCol))
                                { 
                                    if (_cellState[iSubRow][idSubCol])
                                    {
                                        nbAliveNeighbour++;
                                    }
                                }
                            }
                        }
                    }

                    if (currCellIsAlive)
                    {
                        if ((nbAliveNeighbour == 2) || (nbAliveNeighbour == 3))
                        {
                            _cellStateNextGeneration[idRow][idCol] = true;
                        }
                    }
                    else//cell is dead
                    {
                        if (nbAliveNeighbour == 3)
                        {
                            _cellStateNextGeneration[idRow][idCol] = true;
                        }
                    }
                }
            }

            _cellStateTmp = _cellStateNextGeneration;
            _cellStateNextGeneration = _cellState;
            _cellState = _cellStateTmp;
            pCptGeneration++;
        }

        private List<Point> getListAliveCell()
        {
            List<Point> listAliveCell = new List<Point>();

            for (int i = 0; i < _nbRow; i++)
            {
                for (int j= 0; j < _nbCol; j++)
                {
                    if (_cellState[i][j])
                    {
                        listAliveCell.Add(new Point(i, j));
                    }
                }
            }

            return listAliveCell;
        }

    }
}
