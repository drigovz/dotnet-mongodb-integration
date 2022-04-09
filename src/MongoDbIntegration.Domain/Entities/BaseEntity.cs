using FluentValidation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace MongoDbIntegration.Domain.Entities
{
    public abstract class BaseEntity 
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public ObjectId Id { get; private set; } = ObjectId.GenerateNewId();
        
        private DateTime? _createdAt;
        public DateTime? CreatedAt
        {
            get => _createdAt;
            set => _createdAt = (value == null ? DateTime.UtcNow : value);
        }
        
        public DateTime UpdatedAt { get; set; }
        
        [NotMapped]
        [JsonIgnore]
        public bool Valid { get; protected set; }
        
        [NotMapped]
        [JsonIgnore]
        public ValidationResult? ValidationResult { get; protected set; }
        
        protected bool EntityValidation<T>(T model, AbstractValidator<T> validator)
        {
            ValidationResult = validator.Validate(model);
            return Valid = ValidationResult.IsValid;
        }
    }
}