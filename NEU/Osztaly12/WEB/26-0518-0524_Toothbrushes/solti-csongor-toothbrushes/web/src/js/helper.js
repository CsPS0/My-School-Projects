const priceFormatOptions = {
  style: "currency",
  currency: "HUF",
  maximumFractionDigits: 0,
  minimumFractionDigits: 0,
};

export function formatPrice(price) {
  return new Intl.NumberFormat("hu-HU", priceFormatOptions).format(price);
}
