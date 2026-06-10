using System;
using System.Text.RegularExpressions;
using RusztikusAdmin.Models;

namespace RusztikusAdmin.Services
{
    public class ValidationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Field { get; set; }
    }

    public class ValidationService
    {
        public ValidationResult ValidateBooking(Booking booking)
        {
            // Név validáció
            if (string.IsNullOrWhiteSpace(booking.Name) || booking.Name.Length < 3)
            {
                return new ValidationResult
                {
                    Success = false,
                    Message = "A név legalább 3 karakter hosszú legyen!",
                    Field = "Name"
                };
            }

            // Email validáció
            if (!string.IsNullOrWhiteSpace(booking.Email))
            {
                var emailRegex = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");
                if (!emailRegex.IsMatch(booking.Email))
                {
                    return new ValidationResult
                    {
                        Success = false,
                        Message = "Kérjük, adjon meg egy érvényes email címet!",
                        Field = "Email"
                    };
                }
            }

            // Telefonszám validáció
            var phoneRegex = new Regex(@"^(\+36|06)[\s-]?\d{1,2}[\s-]?\d{3}[\s-]?\d{4}$");
            if (!phoneRegex.IsMatch(booking.Phone))
            {
                return new ValidationResult
                {
                    Success = false,
                    Message = "Érvényes magyar telefonszámot adjon meg! (pl. +36 20 123 4567)",
                    Field = "Phone"
                };
            }

            // Dátum validáció
            if (!DateTime.TryParse(booking.Date, out DateTime bookingDate))
            {
                return new ValidationResult
                {
                    Success = false,
                    Message = "Érvénytelen dátum formátum!",
                    Field = "Date"
                };
            }

            // Vendégszám validáció
            if (booking.Guests < 1 || booking.Guests > 20)
            {
                return new ValidationResult
                {
                    Success = false,
                    Message = "A vendégek száma 1 és 20 között kell legyen!",
                    Field = "Guests"
                };
            }

            // Asztalszám validáció
            if (booking.TableNumber < 1)
            {
                return new ValidationResult
                {
                    Success = false,
                    Message = "Kérjük, válasszon egy asztalt!",
                    Field = "TableNumber"
                };
            }

            return new ValidationResult
            {
                Success = true,
                Message = "Foglalás sikeresen validálva!"
            };
        }

        public ValidationResult ValidateMenuItem(MenuItem menuItem)
        {
            if (string.IsNullOrWhiteSpace(menuItem.Name) || menuItem.Name.Length < 3)
            {
                return new ValidationResult
                {
                    Success = false,
                    Message = "A menüelem neve legalább 3 karakter hosszú legyen!",
                    Field = "Name"
                };
            }

            if (menuItem.Price < 0)
            {
                return new ValidationResult
                {
                    Success = false,
                    Message = "Az ár nem lehet negatív!",
                    Field = "Price"
                };
            }

            return new ValidationResult
            {
                Success = true,
                Message = "Menüelem sikeresen validálva!"
            };
        }
    }
}