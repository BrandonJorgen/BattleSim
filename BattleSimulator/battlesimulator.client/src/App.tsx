import { useState } from 'react'
import "./app.css"
import BattlerGrid from './Components/BattlerGrid/BattlerGrid'


function App() {
    const [statusText, setStatusText] = useState('Choose your battlers!')
    const [battleType, setBattleType] = useState(0)
    const [battleCount, setBattleCount] = useState(0)

    const HandleBattleTypeSelect = (event: React.ChangeEvent<HTMLSelectElement>) => {
        if (event.target != null)
            setBattleType(Number(event.target.value));
    }

    async function StartBattle(mode: number) {
        const response = await fetch('battlesimulator/Battle', {
            method: 'POST',
            body: JSON.stringify(mode),
            headers: {
                'Content-Type': 'application/json',
            },
        });
        if (response.ok) {
            const data = await response.text();
            setStatusText(data);

            if (data[0] != "E")
                setBattleCount(battleCount + 1);
        }
    }

    return (
        <div>
            <div className="master-grid">
                <select className="battlemode-select" onChange={HandleBattleTypeSelect}>
                    <option value={0}>1v1</option>
                    <option value={1}>2v2</option>
                    <option value={2}>3v3</option>
                    <option value={3}>4v4</option>
                </select>
                <BattlerGrid battleType={battleType} battleCount={battleCount} />
                <button className="fight-button" onClick={() => StartBattle(battleType)}>FIGHT</button>
                <b className="status-text">{statusText}</b>
            </div>
        </div>
    );
}

export default App;