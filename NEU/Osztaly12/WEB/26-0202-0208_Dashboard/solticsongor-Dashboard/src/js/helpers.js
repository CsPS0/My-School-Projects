export function formatEmoji(code) {
  return String.fromCodePoint(parseInt(code.replace('U+', ''), 16));
}