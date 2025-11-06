import docx
import sys
import os

def read_docx_text(path):
    try:
        document = docx.Document(path)
        return "\n".join([para.text for para in document.paragraphs])
    except Exception as e:
        return f"Error reading {path}: {e}"

if __name__ == "__main__":
    if len(sys.argv) > 1:
        for path in sys.argv[1:]:
            if os.path.isdir(path):
                for root, _, files in os.walk(path):
                    for file in files:
                        if file.endswith(".docx"):
                            filepath = os.path.join(root, file)
                            print(f"--- {filepath} ---")
                            print(read_docx_text(filepath))
            elif path.endswith(".docx"):
                print(f"--- {path} ---")
                print(read_docx_text(path))
    else:
        print("Usage: python read_docx.py <file_or_directory_path> ...")