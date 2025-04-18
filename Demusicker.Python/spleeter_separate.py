import sys
from spleeter.separator import Separator
from pathlib import Path

def main():
    if len(sys.argv) < 3:
        print("Usage: spleeter_separate.py <input_file> <output_dir>")
        sys.exit(1)

    input_file = sys.argv[1]
    output_dir = sys.argv[2]

    if not Path(input_file).is_file():
        print(f"Error: Input file '{input_file}' does not exist.", file=sys.stderr)
        sys.exit(2)

    Path(output_dir).mkdir(parents=True, exist_ok=True)

    separator = Separator('spleeter:4stems')
    separator.separate_to_file(input_file, output_dir, filename_format = "{instrument}.{codec}",)

    print("Separation completed successfully.")

if __name__ == '__main__':
    main()
