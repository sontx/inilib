using System.Collections;
using System.Collections.Generic;
using System.IO;
using INILib.Exception;

namespace INILib
{
    public sealed class INIFile : IList<Section>
    {
        private List<Section> _sections = new List<Section>();
        private string _filePath;

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = string.IsNullOrEmpty(value) ? null : value.Trim();
                if (!string.IsNullOrEmpty(_filePath))
                    LoadFile();
            }
        }

        public INIFile()
        {
            FilePath = null;
        }

        public INIFile(string filePath)
        {
            FilePath = filePath;
        }

        #region helper methods

        private string BuildSectionString(string sectionName)
        {
            return string.Format("{0}{1}{2}", Config.SECTION_CHARS[0], sectionName, Config.SECTION_CHARS[1]);
        }

        private string BuildKeyString(string name, string value)
        {
            return string.Format("{0}{1}{2}", name, Config.KEY_CHAR, value);
        }

        private bool IsSectionHeader(string line)
        {
            return line.StartsWith(Config.SECTION_CHARS[0]) && line.EndsWith(Config.SECTION_CHARS[1]);
        }

        private bool IsCommentLine(string line)
        {
            foreach (var commentChar in Config.COMMENT_CHARS)
            {
                if (line.StartsWith(commentChar))
                    return true;
            }
            return false;
        }

        private bool IsBlankLine(string line)
        {
            return line.Trim() == string.Empty;
        }

        private bool TryParseKey(string line, out string keyName, out string value)
        {
            int equalCharIndex = line.IndexOf(Config.KEY_CHAR);
            if (equalCharIndex < 0)
            {
                keyName = null;
                value = null;
                return false;
            }
            keyName = line.Substring(0, equalCharIndex).Trim();
            value = line.Substring(equalCharIndex + 1).Trim();
            return true;
        }

        private string ParseSection(string line)
        {
            return line.Substring(1, line.Length - 2).Trim();
        }

        private void LoadFile()
        {
            lock (this)
            {
                _sections.Clear();
                using (var reader = new StreamReader(FilePath, Config.TEXT_ENCODING, true))
                {
                    Section section = null;
                    int counter = 0;
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine().Trim();
                        counter++;
                        if (!IsBlankLine(line) && !IsCommentLine(line))
                        {
                            if (IsSectionHeader(line))
                            {
                                if (section != null)
                                    _sections.Add(section);
                                section = new Section();
                                section.Name = ParseSection(line);
                            }
                            else
                            {
                                if (section == null)
                                    section = new Section();
                                string keyName;
                                string value;
                                if (TryParseKey(line, out keyName, out value))
                                    section.Keys.Add(keyName, value);
                                else
                                    throw new INIFileFormatException(string.Format("Line {0} has wrong format: {1}", counter, line));
                            }
                        }
                    }
                    if (section != null)
                        _sections.Add(section);
                }
            }
        }

        #endregion

        public Section this[string name]
        {
            get
            {
                lock (this)
                {
                    foreach (var section in _sections)
                    {
                        if (string.Compare(section.Name, name, Config.IGNORE_CASE) == 0)
                            return section;
                    }
                    return null;
                }
            }
            set
            {
                lock (this)
                {
                    for (int i = 0; i < _sections.Count; i++)
                    {
                        if (string.Compare(_sections[i].Name, name, Config.IGNORE_CASE) == 0)
                        {
                            _sections[i] = value;
                        }
                    }
                }
            }
        }

        public void Save()
        {
            SaveAs(_filePath);
        }

        public void SaveAs(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new NullElementException("INI File path is null or empty.");
            using (var writer = new StreamWriter(filePath, false, Config.TEXT_ENCODING))
            {
                lock (this)
                {
                    foreach (var section in _sections)
                    {
                        if (!string.IsNullOrEmpty(section.Name))
                            writer.WriteLine(BuildSectionString(section.Name));
                        foreach (var key in section.Keys)
                        {
                            writer.WriteLine(BuildKeyString(key.Key, key.Value));
                        }
                    }
                }
            }
        }

        public void Add(string sectionName, string keyName, string value)
        {
            lock (this)
            {
                Section section = this[sectionName];
                if (section == null)
                {
                    section = new Section();
                    section.Name = sectionName;
                    _sections.Add(section);
                }
                section.Keys.Add(keyName, value);
            }
        }

        #region implements IList<Section>

        public int Count { get { lock (this) { return _sections.Count; } } }

        public bool IsReadOnly { get { return false; } }

        public Section this[int index]
        {
            get { lock (this) { return _sections[index]; } }
            set { lock (this) { _sections[index] = value; } }
        }

        public int IndexOf(Section section)
        {
            lock (this)
            {
                return _sections.IndexOf(section);
            }
        }

        public void Insert(int index, Section section)
        {
            lock (this)
            {
                _sections.Insert(index, section);
            }
        }

        public void RemoveAt(int index)
        {
            lock (this)
            {
                _sections.RemoveAt(index);
            }
        }

        public void Add(Section section)
        {
            lock (this)
            {
                _sections.Add(section);
            }
        }

        public void Clear()
        {
            lock (this)
            {
                _sections.Clear();
            }
        }

        public bool Contains(Section section)
        {
            lock (this)
            {
                return _sections.Contains(section);
            }
        }

        public void CopyTo(Section[] array, int arrayIndex)
        {
            lock (this)
            {
                _sections.CopyTo(array, arrayIndex);
            }
        }

        public bool Remove(Section section)
        {
            lock (this)
            {
                return _sections.Remove(section);
            }
        }

        public IEnumerator<Section> GetEnumerator()
        {
            return _sections.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _sections.GetEnumerator();
        }

        #endregion
    }
}
