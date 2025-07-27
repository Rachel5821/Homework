// DBEntities.Models.Supplier - רק זה דורש שינוי
using DBEntities.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }
    public string? ManufacturerName { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public string RepresentativeName { get; set; } = null!;
    public int? UserId { get; set; }  // שיניתי שם מ-Id ל-UserId
    public virtual User? User { get; set; }  // שיניתי שם מ-IdNavigation ל-User
}