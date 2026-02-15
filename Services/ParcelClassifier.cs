using ParcelDelivery.Api.Interfaces;
using ParcelDelivery.Api.Models;

namespace ParcelDelivery.Api.Services;

public class ParcelClassifier : IParcelClassifier
{
    // Current business rules:
    // - up to 1 kg => Mail
    // - up to 10 kg => Regular
    // - over 10 kg => Heavy
    // Seeded department IDs (must match the seeded values in DbContext migration)
    public static readonly Guid MailId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    public static readonly Guid RegularId = Guid.Parse("22222222-2222-2222-2222-222222222222");
    public static readonly Guid HeavyId = Guid.Parse("33333333-3333-3333-3333-333333333333");

    public Guid Classify(double weightKg)
    {
        if (weightKg <= 1.0) return MailId;
        if (weightKg <= 10.0) return RegularId;
        return HeavyId;
    }
}
