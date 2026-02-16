using ParcelDelivery.Api.Interfaces;
using ParcelDelivery.Api.Models;

namespace ParcelDelivery.Api.Services;

public class ParcelClassifier : IParcelClassifier
{
    // Current business rules:
    // - up to 1 kg => Mail
    // - up to 10 kg => Regular
    // - over 10 kg => Heavy
    public Department ClassifyDepartment(double weightKg)
    {
        if (weightKg <= 1.0) return Department.Mail;
        if (weightKg <= 10.0) return Department.Regular;
        return Department.Heavy;
    }
}
