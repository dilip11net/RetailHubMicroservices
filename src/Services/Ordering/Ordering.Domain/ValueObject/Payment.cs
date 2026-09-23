namespace Ordering.Domain.ValueObject
{
    public record Payment
    {
        public string CardName { get; init; } = default!;

        public string CardNumber { get; init; } = default!;



        public string Expiration { get; init; } = default!;

        public string CVV { get; init; } = default!;

        public int PaymentMethod { get; init; } = default!;

        protected Payment() { }

        private Payment(string cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            Expiration = expiration;
            CVV = cvv;
            PaymentMethod = paymentMethod;
        }
        public static Payment Of(string cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
        {
            ArgumentException.ThrowIfNullOrEmpty(cardName, nameof(cardName));
            ArgumentException.ThrowIfNullOrEmpty(cardNumber, nameof(cardNumber));
            ArgumentException.ThrowIfNullOrEmpty(expiration, nameof(expiration));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3, nameof(cvv));
            return new Payment(cardName, cardNumber, expiration, cvv, paymentMethod);
        }


    }
}
