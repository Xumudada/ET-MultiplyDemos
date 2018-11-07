namespace ETModel
{
    /// <summary>
    /// 参考Unit类
    /// </summary>
    public partial class Card
    {
        public static Card Create(int weight, int suits)
        {
            Card card = new Card();
            card.CardWeight = weight;
            card.CardSuits = suits;
            return card;
        }

        public bool Equals(Card other)
        {
            return this.CardWeight == other.CardWeight && this.CardSuits == other.CardSuits;
        }

        /// <summary>
        /// 获取卡牌名
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return this.CardSuits == (int)Suits.None ? ((Weight)this.CardWeight).ToString() : $"{((Suits)this.CardSuits).ToString()}{((Weight)this.CardWeight).ToString()}";
        }
    }

}



    
   
