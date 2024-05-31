export default async function chooseFile(accept: string): Promise<File> {
  const input = document.createElement('input');
  input.type = 'file';
  input.accept = accept;
  input.click();

  return new Promise<File>((resolve, reject) => {
    input.onchange = async () => {
      const file = input.files?.item(0) ?? false;
      if(!file) return;
      resolve(file);
    };
    input.onerror = (e) => reject(e);
  });
}