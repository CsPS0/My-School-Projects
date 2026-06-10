// Validation function - MUST return object with success and message
function validateBooking(booking) {
  // Name validation
  if (!booking.name || booking.name.trim().length < 3) {
    return {
      success: false,
      message: 'A név legalább 3 karakter hosszú legyen!',
      field: 'name'
    };
  }

  // Email validation (optional but if provided must be valid)
  if (booking.email) {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(booking.email)) {
      return {
        success: false,
        message: 'Kérjük, adjon meg egy érvényes email címet!',
        field: 'email'
      };
    }
  }

  // Phone validation - Hungarian format
  const phoneRegex = /^(\+36|06)[\s-]?\d{1,2}[\s-]?\d{3}[\s-]?\d{4}$/;
  if (!phoneRegex.test(booking.phone)) {
    return {
      success: false,
      message: 'Érvényes magyar telefonszámot adjon meg! (pl. +36 20 123 4567)',
      field: 'phone'
    };
  }

  // Date validation
  const bookingDate = new Date(booking.date);
  const today = new Date();
  today.setHours(0, 0, 0, 0);

  if (isNaN(bookingDate.getTime())) {
    return {
      success: false,
      message: 'Érvénytelen dátum formátum!',
      field: 'date'
    };
  }

  if (bookingDate < today) {
    return {
      success: false,
      message: 'A foglalás dátuma nem lehet múltbeli!',
      field: 'date'
    };
  }

  // Guests validation
  if (booking.guests < 1 || booking.guests > 20) {
    return {
      success: false,
      message: 'A vendégek száma 1 és 20 között kell legyen!',
      field: 'guests'
    };
  }

  // SUCCESS
  return {
    success: true,
    message: 'Foglalás sikeresen validálva!'
  };
}