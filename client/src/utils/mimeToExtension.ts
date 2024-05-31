export default function mimeToExtension(mime: string): string {
  return mime.split('/').pop() ?? '';
}