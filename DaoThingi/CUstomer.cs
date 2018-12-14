namespace DaoThingi
{
    public class Customer
    {
        private string _CustomerFirstName;
        private string _CustomerLastName;
        private string _CustomerIDNumber;

        public Customer()
        {
            _CustomerFirstName = "";
            _CustomerLastName = "";
            _CustomerIDNumber = "";
        }

        public string CustomerFirstName
        {
            [DAO("FirstName", typeof(string), false)]
            get
            {
                return _CustomerFirstName;
            }
            set
            {
                _CustomerFirstName = value;
            }
        }

        public string CustomerLastName
        {
            [DAO("LastName", typeof(string), false)]
            get
            {
                return _CustomerLastName;
            }
            set
            {
                _CustomerLastName = value;
            }
        }

        public string CustomerIDNumber
        {
            [DAO("CustID", typeof(string), true)]  
            get
            {
                return _CustomerIDNumber;
            }
            set
            {
                _CustomerIDNumber = value;
            }
        }
    }
}
