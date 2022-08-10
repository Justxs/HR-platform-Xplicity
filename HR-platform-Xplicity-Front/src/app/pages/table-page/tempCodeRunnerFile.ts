myUploader(event: any){
  const target: DataTransfer = <DataTransfer>(event);
  const reader: FileReader = new FileReader();
  reader.onload = (e: any) => {
    const bstr: string = e.target.result;
    const wb: XLSX.WorkBook = XLSX.read(bstr, { type: 'binary' });

    const wsname: string = wb.SheetNames[0];
    const ws: XLSX.WorkSheet = wb.Sheets[wsname];

    this.data = <AOA>(XLSX.utils.sheet_to_json(ws, { header: 1 }));
    let json: Candidate[] = [];
    this.data.forEach( function (value) {
      if(value[1] == null){
        return;
      }
      json.push(formatJson(value));
    });
    json.splice(0, 1);
    console.log(JSON.stringify(json));
    
  };
  reader.readAsBinaryString(target.files[0]);
  
};