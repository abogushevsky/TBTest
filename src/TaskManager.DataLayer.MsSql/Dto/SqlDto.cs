namespace TaskManager.DataLayer.MsSql.Dto
{
    public abstract class SqlDto
    {
        public abstract object GetParametersForInsert();

        public abstract object GetParametersForUpdate();
    }
}