import React, { useEffect, useState } from "react";
import axios from "axios"
import App2 from "./App2";
const FromCsv = () => {

  const [csvData, setCsvData] = useState(null);
  const [csvNames, setCsvNames] = useState([])
  const [currentCsvName, setCurrentCsvName] = useState(null)
  let fileName;
  let objectifiedArray;
  let objectifiedArrayForDatabase;
  const postCsvAsync = () => {
    axios.post("https://localhost:7066/api/CSV",
      { csvName: fileName, tasks: objectifiedArrayForDatabase }).then(res => console.log(res)
      )
  }

  console.log(csvData)
  const processCSV = (file) => {
    const reader = new FileReader();
    reader.onload = (e) => {
      const content = e.target.result;
      console.log(e.target)
      const lines = content.split("\n");
      console.log(lines)
      let data = lines.map((line) => line.split(","));
      console.log(data)


      data = data.slice(1, -1)
      objectifiedArrayForDatabase = data.map((element, index) => {
        return {
          name: element[0],
          startDate: element[1],
          endDate: element[2],
        };
      });
      console.log(objectifiedArrayForDatabase)
      objectifiedArray = data.map((element, index) => {

        return {
          id: index,
          title: element[0],
          start: new Date(element[1].split(".")[2],
            element[1].split(".")[1] - 1,
            element[1].split(".")[0]),
          end: new Date(element[2].split(".")[2],
            element[2].split(".")[1] - 1,
            element[2].split(".")[0]),
          percentComplete: 1
        };
      });
      console.log(objectifiedArray)
      postCsvAsync();
    };
    reader.readAsText(file);
  };

  const getCSVTasks = async () => {

    let rawTaskData;
    const params = {
      csvName: currentCsvName
    }
    console.log(params.csvName + " asad")
    axios.get(`https://localhost:7066/api/CSV?csvName=${currentCsvName}`)
      .then(res => {
        var x = res.data.map((element, index) => {
          console.log(2, element);
          return {
            id: index,
            title: element.name,
            start: new Date(element.startDate),
            end: new Date(element.endDate),
            percentComplete: 1
          };
        });
        console.log(1, x,  res.data)
        setCsvData(x)
      })
      .catch(e => console.log(e))
    console.log(rawTaskData)


  }

  const CsvToolBar = () => {
    if (csvNames.length != 0) {
      console.log(csvNames)
      const options = csvNames.map(item => <option key={item} value={item}>{item}</option>);
      return (
        <select value={currentCsvName} onChange={handleChange} style={selectStyle}>
          {options}
        </select>
      );
    } else {
      return (
        <div style={selectStyle} disabled>
          No saved item
        </div>
      );
    }

  }
  const handleChange = (event) => {
    console.log(event.target.value)
    setCurrentCsvName(event.target.value);

  };

  const getCsvNames = () => {
    axios.get("https://localhost:7066/getCSVNames").then(res => setCsvNames(res.data))

  }

  // Handle the click event of the import button
  const handleImport = (e) => {
    const file = e.target.files[0];
    fileName = file.name.replace(/\s+/g, '');
    if (file) {
      processCSV(file);
    } else {

    }
  }
  const selectStyle = {
    padding: '8px',
    fontSize: '16px',
    borderRadius: '5px',
    border: '1px solid #ccc',
    outline: 'none',
    width: '200px',
  };
  useEffect(() => {
    if (currentCsvName)
      getCSVTasks();

  }, [currentCsvName])
  useEffect(() => {

    getCsvNames();

  }, [])
  return (
    <div>


      {csvData ? <App2 csvdata={csvData} /> : (
        <div>
          <p>Please select a CSV file to import.</p>
          <input type="file" accept=".csv" onChange={handleImport} />
          <CsvToolBar />
        </div>
      )}
    </div>
  );
};

export default FromCsv;
