interface UseFileReaderProp {
  accept?: string;
}
export default async function useFileReader({
  accept,
}: UseFileReaderProp) {


  const input = document.createElement('input');
  input.type = 'file';
  input.accept = accept ?? '';
  input.onchange = async () => {
    const file = input.files?.item(0) ?? false;
    if(!file) return;
    // await onFileSelected(file);
  };

  const open = () => {
    if (!input.files?.length)
      input.click();
  };

  return { open };
}