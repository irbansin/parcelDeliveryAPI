using ParcelDelivery.Api.Models;

namespace ParcelDelivery.Api.Interfaces;

public interface IParcelClassifier
{
    Department ClassifyDepartment(double weightKg, decimal value);

    ApprovalStatus ClassifyApproval(decimal value);
    
}
