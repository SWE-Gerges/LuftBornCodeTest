import { useEffect, useState } from 'react';
import './App.css';

function App() {
    const [publishers, setPublishers] = useState();

    useEffect(() => {
        populatePublishersData();
    }, []);

    const contents = publishers === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started.</em></p>
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Publisher Name</th>
                    
                </tr>
            </thead>
            <tbody>
                {publishers.map(publishers =>
                    <tr key={publishers.Name}>
                        <td>{publishers.Name}</td>
              
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tabelLabel">Publishers</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>
    );
    
    async function populatePublishersData() {
        const response = await fetch('publishers');
        const data = await response.json();
        setPublishers(data);
    }
}

export default App;