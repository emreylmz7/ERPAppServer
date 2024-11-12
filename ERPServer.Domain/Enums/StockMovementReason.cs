namespace ERPServer.Domain.Enums;

public enum StockMovementReason
{
    Sale = 1,           // Satış
    Purchase,       // Alış
    Adjustment,     // Ayarlama (stok düzeltmesi)
    Transfer,       // Transfer (depolar arası taşıma)
    Return          // İade
}
