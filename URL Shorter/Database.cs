using SQLite;

namespace URL_Shorter
{
    class Database
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Target { get; set; }
    }
}
