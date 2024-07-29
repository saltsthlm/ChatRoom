type DataRow = {
    values: (number | string | Date)[];
    key: number;
};

type DataHeader = {
    label: string;
};

type Props = {
    headers: DataHeader[];
    rows: DataRow[];
    label: string;
};

function DataTable(props: Props) {
    return (
        <div className="overflow-x-auto">
            <h2>{props.label}</h2>
            <table className="table table-xs">
                <thead>
                    <tr>
                        {props.headers.map((header) => (
                            <th>{header.label}</th>
                        ))}
                    </tr>
                </thead>
                <tbody>
                    {props.rows.map((row) => {
                        return (
                            <tr key={row.key}>
                                {row.values.map((value) => (
                                    <td>{value.toString()}</td>
                                ))}
                            </tr>
                        );
                    })}
                </tbody>
            </table>
        </div>
    );
}

export default DataTable;
