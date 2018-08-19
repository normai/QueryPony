// id          : 20130717°0601
// encoding    : UTF-8-with-BOM

namespace Divan
{
    public interface ICouchViewDefinitionBase
    {
        ICouchDatabase Db();
        CouchDesignDocument Doc { get; set; }
        string Name { get; set; }
        string Path();
        ICouchRequest Request();
    }
}
