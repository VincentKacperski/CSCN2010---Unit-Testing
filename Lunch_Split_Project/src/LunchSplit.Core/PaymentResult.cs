public record PaymentResult (
    bool Success,
    string? PaymentId,
    string? Status,
    string? ErrorMessage
);