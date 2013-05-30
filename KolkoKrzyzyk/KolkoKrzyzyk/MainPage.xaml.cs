using KolkoKrzyzyk.Action;
using KolkoKrzyzyk.Static;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace KolkoKrzyzyk
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : KolkoKrzyzyk.Common.LayoutAwarePage
    {
        public int[][] Tablica = new int[3][];
        #region inicjalizacja wielu zmiennych jak
        public MainPage()
        {
            this.InitializeComponent();
            //Wpisanie nazw graczy
            txtSecondPlayer.Text = MainSettings.Name2;
            txtFirstPlayer.Text = MainSettings.Name1;
            if (MainSettings.Game)
            {
                //inicjalizacja tablicy z wynikami
                for (int i = 0; i < 3; i++)
                    Tablica[i] = new int[3];
                for (int i = 0; i < 3; i++)
                    for (int l = 0; l < 3; l++)
                        Tablica[i][l] = -1;
                //Określenie kto teraz gra
                txtWhoPlay.Text = "Teraz gra " + MainSettings.Name1;
            }
           
        }
        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            MainSettings.Game = true;
            MainSettings.WhichPlayer = true;
            _00.Source = imgexEmpty.Source;
            _01.Source = imgexEmpty.Source;
            _02.Source = imgexEmpty.Source;
            _10.Source = imgexEmpty.Source;
            _11.Source = imgexEmpty.Source;
            _12.Source = imgexEmpty.Source;
            _20.Source = imgexEmpty.Source;
            _21.Source = imgexEmpty.Source;
            _22.Source = imgexEmpty.Source;

            //inicjalizacja tablicy z wynikami
            for (int i = 0; i < 3; i++)
                Tablica[i] = new int[3];
            for (int i = 0; i < 3; i++)
                for (int l = 0; l < 3; l++)
                    Tablica[i][l] = -1;
            //Określenie kto teraz gra
            txtWhoPlay.Text = "Teraz gra " + MainSettings.Name1;

        }
        #endregion
        #region wydarzenia dotyczace nawigacji
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CreateGame));
        }
        private void btnOption_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreateGame));
        }
        #endregion

        #region zdarzenia naciśniecia
        private void btn00_Click(object sender, RoutedEventArgs e)
        {
            Button guzik = (Button)sender;
            Action(guzik.Name.Replace("btn", ""));
        }
        #endregion

        #region logika
        //główny trzon logiki
        public void Action(string name)
        {
            //1
            //sprawdzenie czy sie gra
            if (MainSettings.Game)
            {
                if (AddSource(name))
                    if (CheckStatusGame())
                    {
                        if (MainSettings.WhichPlayer)
                            txtWhoPlay.Text = "Wygrał : " + MainSettings.Name2;
                        else
                            txtWhoPlay.Text = "Wygrał : " + MainSettings.Name1;

                        MainSettings.Game = false;
                    }
                    else
                        if (CheckFullTable())
                            txtWhoPlay.Text = "Nikt nie wygrał";
                        else
                            if (MainSettings.WhichPlayer)
                            {
                                txtWhoPlay.Text = "Teraz gra " + MainSettings.Name1;
                            }
                            else
                            {
                                txtWhoPlay.Text = "Teraz gra " + MainSettings.Name2;
                            }

            }

        }
        //Sprawdzenie czy ktoś nie wygrał
        public bool CheckStatusGame()
        {
            #region poziome warianty
            //1 linia pozioma
            if (Tablica[0][0] == Tablica[0][1] && Tablica[0][1] == Tablica[0][2] && Tablica[0][0] != -1)
                return true;
            //2 linia pozioma
            if (Tablica[1][0] == Tablica[1][1] && Tablica[1][1] == Tablica[1][2] && Tablica[1][0] != -1)
                return true;
            //3 linia pozioma
            if (Tablica[2][0] == Tablica[2][1] && Tablica[2][1] == Tablica[2][2] && Tablica[2][0] != -1)
                return true;
            #endregion

            #region pionowe warianty
            //1 linia pionowa
            if (Tablica[0][0] == Tablica[1][0] && Tablica[1][0] == Tablica[2][0] && Tablica[0][0] != -1)
                return true;
            //2 linia pionowa
            if (Tablica[0][1] == Tablica[1][1] && Tablica[1][1] == Tablica[2][1] && Tablica[0][1] != -1)
                return true;
            //3 linia pionowa
            if (Tablica[0][2] == Tablica[1][2] && Tablica[1][2] == Tablica[2][2] && Tablica[0][2] != -1)
                return true;
            #endregion

            #region krzyzowe warianty
            if (Tablica[0][0] == Tablica[1][1] && Tablica[1][1] == Tablica[2][2] && Tablica[0][0] != -1)
                return true;
            if (Tablica[0][2] == Tablica[1][1] && Tablica[1][1] == Tablica[2][0] && Tablica[0][2] != -1)
                return true;
            #endregion

            return false;
        }
        //sprawdzenie czy tablica jest zapełniona
        public bool CheckFullTable()
        {
            for (int i = 0; i < 3; i++)
                for (int l = 0; l < 3; l++)
                    if (Tablica[i][l] == -1)
                        return false;
            return true;
        }
        //wprowadzanie wybranego pola gracza
        public bool AddSource(string where)
        {
            //inicjalizacja zmiennych
            int x = -1, y = -1;
            //2
            #region przygotowanie obrazka do wsadzenia
            ImageSource s = imgexEmpty.Source;
            if (MainSettings.WhichPlayer)
            {
                s = imgexCross.Source;
            }
            else
            {
                s = imgexCircle.Source;
            }
            #endregion

            //3
            #region pobieranie pozycji
            switch (where)
            {
                case "00":
                    x = 0;
                    y = 0;
                    break;
                case "01":
                    x = 0;
                    y = 1;
                    break;
                case "02":
                    x = 0;
                    y = 2;
                    break;
                case "10":
                    x = 1;
                    y = 0;
                    break;
                case "11":
                    x = 1;
                    y = 1;
                    break;
                case "12":
                    x = 1;
                    y = 2;
                    break;
                case "20":
                    x = 2;
                    y = 0;
                    break;
                case "21":
                    x = 2;
                    y = 1;
                    break;
                case "22":
                    x = 2;
                    y = 2;
                    break;
            }
            #endregion


            //4
            #region sprawdzenie czy natej pozycji jest już znak
            if (x != -1 && y != -1)
            {
                if (Tablica[y][x] == -1)
                {
                    //5
                    #region wprowadzenie odpowiedniego znaku
                    if (MainSettings.WhichPlayer) Tablica[y][x] = 1;
                    else Tablica[y][x] = 2;
                    #endregion

                    //6
                    #region wprowadzenie znaku do grid-u
                    switch (where)
                    {
                        case "00":
                            _00.Source = s;
                            break;
                        case "01":
                            _01.Source = s;
                            break;
                        case "02":
                            _02.Source = s;
                            break;
                        case "10":
                            _10.Source = s;
                            break;
                        case "11":
                            _11.Source = s;
                            break;
                        case "12":
                            _12.Source = s;
                            break;
                        case "20":
                            _20.Source = s;
                            break;
                        case "21":
                            _21.Source = s;
                            break;
                        case "22":
                            _22.Source = s;
                            break;
                    }
                    #endregion


                    MainSettings.WhichPlayer = !MainSettings.WhichPlayer;

                    return true;
                }
                else
                    return false;
            }
            #endregion

            return false;
        }
        #endregion 
    }
}