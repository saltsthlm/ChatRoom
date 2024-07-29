import "./App.css";
import DataTable from "./components/DataTable";

function App() {
    return (
        <>
            <DataTable
                headers={[{ label: "Id" }, { label: "Alias" }, { label: "Password" }]}
                rows={[
                    { values: [1, "Adam", "qwerty"], key: 0 },
                    { values: [2, "Bob", "1234"], key: 1 },
                    { values: [3, "Catherine", "password"], key: 2 },
                ]}
                label="Users"
            ></DataTable>
        </>
    );
}

export default App;
