﻿using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Poker.Cards;
using Poker.Cards.Views;
using Poker.Models;
using Poker.Cards.Views;

namespace Poker.Views {
    /// <summary>
    /// Interaction logic for PokerCardView.xaml
    /// </summary>
    public partial class PokerCardView : UserControl {

        public static readonly RoutedEvent CardClickEvent = EventManager.RegisterRoutedEvent("CardClick", RoutingStrategy.Direct, typeof(CardEventHandler), typeof(PokerCardView));

        private const double DIM_X = 2;
        private const double DIM_Y = 2.5;

        public PokerCardView() {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
            var oldCard = e.OldValue as PokerCardViewModel;
            if (oldCard != null) {
                oldCard.PropertyChanged -= OnCardPropertyChanged;
            }
            var newCard = e.NewValue as PokerCardViewModel;
            if (newCard != null) {
                newCard.PropertyChanged += OnCardPropertyChanged;
            }
            UpdateImage(newCard);
        }

        private void OnCardPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs) {
            switch (propertyChangedEventArgs.PropertyName) {
                case "Rank":
                case "Suit":
                    UpdateImage(DataContext as PokerCardViewModel);
                    break;
            }
        }

        private BaseCardView GetCardView(PokerCard card) {
            switch (card.ToSimpleString()) {
                case "2C":
                    return new C2();
                case "3C":
                    return new C3();
                case "4C":
                    return new C4();
                case "JC":
                    return new CJ();
                case "QC":
                    return new CQ();
                case "9H":
                    return new H9();
                case "10H":
                    return new H10();
                case "QH":
                    return new HQ();
                case "2D":
                    return new D2();
                case "6D":
                    return new D6();
                case "9D":
                    return new D9();
                case "10D":
                    return new D10();
                case "KD":
                    return new DK();
                default:
                    return new Empty();
            }
        }

        private void UpdateImage(PokerCardViewModel model) {
            var cardView = GetCardView(model.Card);
            inner.Child = cardView;
            //try {
            //    var id = GetImageId(model);
            //    var uri = new Uri(string.Format("pack://application:,,,/Resources/Cards/{0}.png", id));
            //    var bi = new BitmapImage(uri);
            //    image.Source = bi;
            //} catch {
            //    image.Source = null;
            //}
        }

        private static string GetImageId(PokerCardViewModel card) {
            var id = "";
            if (card.Suit == CardSuit.None || card.Rank == CardRank.None) return "empty";
            switch (card.Suit) {
                case CardSuit.Clubs:
                    id += 'c';
                    break;
                case CardSuit.Diamonds:
                    id += 'd';
                    break;
                case CardSuit.Spades:
                    id += 's';
                    break;
                case CardSuit.Hearts:
                    id += 'h';
                    break;
            }
            switch (card.Rank) {
                case CardRank.Ace:
                    id += 'a';
                    break;
                case CardRank.Two:
                    id += '2';
                    break;
                case CardRank.Three:
                    id += '3';
                    break;
                case CardRank.Four:
                    id += '4';
                    break;
                case CardRank.Five:
                    id += '5';
                    break;
                case CardRank.Six:
                    id += '6';
                    break;
                case CardRank.Seven:
                    id += '7';
                    break;
                case CardRank.Eight:
                    id += '8';
                    break;
                case CardRank.Nine:
                    id += '9';
                    break;
                case CardRank.Ten:
                    id += "10";
                    break;
                case CardRank.Jack:
                    id += 'j';
                    break;
                case CardRank.Queen:
                    id += 'q';
                    break;
                case CardRank.King:
                    id += 'k';
                    break;
            }
            return id;
        }


        //protected override Size ArrangeOverride(Size arrangeBounds) {
        //    var w = arrangeBounds.Width;
        //    var h = arrangeBounds.Height;
        //    var kw = w / DIM_X;
        //    var kh = h / DIM_Y;
        //    var k = Math.Min(kw, kh);
        //    var hp = (w - k * DIM_X) / 2;
        //    var vp = (h - k * DIM_Y) / 2;
        //    inner.Arrange(new Rect(hp, vp, k * DIM_X, k * DIM_Y));
        //    //inner.Margin = new Thickness(hp, vp, hp, vp);
        //    return arrangeBounds;
        //}

        public event CardEventHandler CardClick {
            add { AddHandler(CardClickEvent, value); }
            remove { RemoveHandler(CardClickEvent, value); }
        }

        private void UserControl_Click(object sender, RoutedEventArgs e) {
            RaiseEvent(new CardClickEventArgs(CardClickEvent, this));
        }

    }
}
