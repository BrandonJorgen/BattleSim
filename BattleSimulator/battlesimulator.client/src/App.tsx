import { useState, useEffect } from 'react'
import Battler from './Components/Battler'
import "./app.css"

interface IBattler {
    name: string,
    faction: string,
    win: number,
    loss: number,
    image: string,
    classname: string,
}

function App() {
    const [battlers, setBattlers] = useState([])
    const [usableBattlers] = useState<IBattler[]>([])
    const [selectedBattlers, setSelectedBattlers] = useState([])
    let values = ["Select your Battler", "Select your Battler", "Select your Battler", "Select your Battler"]
    const [battlersValues, setBattlersValues] = useState(values)
    let indexes = [-1, -1, -1, -1]
    const [battlersIndexes, setBattlersIndexes] = useState(indexes)
    const [statusText, setStatusText] = useState('Choose your battlers!')
    const [battleType, setBattleType] = useState("1v1")
    const [battleModeNumber, setBattleModeNumber] = useState(0)


    useEffect(() => {
        GetBattlers();
    }, []);

    async function GetBattlers() {
        const response = await fetch('battlesimulator/GetBattlers');
        if (response.ok) {
            const data = await response.json();
            setBattlers(data);
            for (let i = 0; i <= data.length - 1; i++) {
                if (usableBattlers.length != data.length) {
                    const tempItem = { name: "", faction: "", win: 0, loss: 0, image: "", classname: "" };
                    tempItem.name = data[i].name;
                    tempItem.faction = data[i].faction;
                    tempItem.win = data[i].win;
                    tempItem.loss = data[i].loss;
                    tempItem.image = data[i].image;
                    tempItem.classname = data[i].battlerClassName;
                    usableBattlers.push(tempItem);
                }
            }
        }
    }

    const HandleBattleTypeSelect = (event: React.ChangeEvent<HTMLSelectElement>) => {
        if (event.target != null) setBattleType(event.target.value);
    }

    useEffect(() => {
        UpdateBattleModeNumber();
    }, [battleType]);

    const UpdateBattleModeNumber = () => {
        switch (battleType) {
            case "1v1":
                setBattleModeNumber(0)
                break;
            case "2v2":
                setBattleModeNumber(1)
                break;
            case "3v3":
                setBattleModeNumber(2)
                break;
            case "4v4":
                setBattleModeNumber(3)
                break;
            default:
                setBattleModeNumber(0)
                break;
        }
    }

    const HandleSelectBattler = (slot: number, event: React.ChangeEvent<HTMLSelectElement>) => {
        values = battlersValues;
        values[slot] = event.target.value;
        setBattlersValues(values);

        if (event.target.value == "Select your Battler") {
            ResetBattlerSlot(slot);
            indexes = battlersIndexes;
            indexes[slot] = -1;
            setBattlersIndexes(indexes);
            setBattlersValues(values);
            const tempSelectedBattlers = [...selectedBattlers];
            tempSelectedBattlers[slot] = battlers[-1];
            setSelectedBattlers(tempSelectedBattlers);
        } else {
            for (let i = 0; i <= battlers.length - 1; i++) {
                if (usableBattlers[i].name == event.target.value) {
                    indexes = battlersIndexes;
                    indexes[slot] = i;
                    setBattlersIndexes(indexes);
                    const tempSelectedBattlers = [...selectedBattlers];
                    tempSelectedBattlers[slot] = battlers[i];
                    setSelectedBattlers(tempSelectedBattlers);
                }
            }
        }
    }

    useEffect(() => {
        if (selectedBattlers != null) {
            UpdateBattlersChoice();
        }
    }, [selectedBattlers]);

    async function UpdateBattlersChoice() {
        await fetch(`battlesimulator/UpdateBattlers`, {
            method: 'POST',
            body: JSON.stringify(selectedBattlers),
            headers: {
                'Content-Type': 'application/json',
            },
        });
    }

    async function StartBattle(mode: number) {
        const response = await fetch('battlesimulator/Battle', {
            method: 'POST',
            body: JSON.stringify( mode ),
            headers: {
                'Content-Type': 'application/json',
            },
        });
        if (response.ok) {
            const data = await response.text();
            setStatusText(data);
            UpdateStats();
        }
    }

    async function UpdateStats() {
        const response = await fetch('battlesimulator/UpdateStats')
        if (response.ok) {
            const data = await response.json();
            setBattlers(data);

            for (let i = 0; i <= data.length - 1; i++) {
                const tempItem = { name: "", faction: "", win: 0, loss: 0, image: "", classname: "" };
                tempItem.name = data[i].name;
                tempItem.faction = data[i].faction;
                tempItem.win = data[i].win;
                tempItem.loss = data[i].loss;
                tempItem.image = data[i].image;
                tempItem.classname = data[i].battlerClassName;
                usableBattlers[i] = tempItem;
            }
        }
    }

    async function ResetBattlerSlot(slot: number) {
        await fetch(`battlesimulator/ResetBattlerSlot`, {
            method: 'POST',
            body: JSON.stringify( slot ),
            headers: {
                'Content-Type': 'application/json',
            },
        });
    }

    return (
        <div>
            <select className="battle-select" onChange={HandleBattleTypeSelect}>
                <option value="1v1">1v1</option>
                <option value="2v2">2v2</option>
                <option value="3v3">3v3</option>
                <option value="4v4">4v4</option>
            </select>
            <div className="master-grid">
                <div className="battler-grid-left">
                    {Array(battleModeNumber + 1).fill(0).map((any, i) => ( 
                        <select key={i} className="battler-select" onChange={(event) => HandleSelectBattler(i, event)} value={battlersValues[i]}>
                            <option key={i} value="Select your Battler">Select your Battler</option>
                            {usableBattlers.map((battler, i) => (<option key={i} value={battler.name}>{battler.name}</option>))}
                        </select>
                    ))}
                    {Array(battleModeNumber + 1).fill(0).map((any, i) => (
                        <div key={i} className="align">
                            {usableBattlers[battlersIndexes[i]] != null ? (<Battler key={i} name={usableBattlers[battlersIndexes[i]].name} faction={usableBattlers[battlersIndexes[i]].faction} winloss={usableBattlers[battlersIndexes[i]].win + "-" + usableBattlers[battlersIndexes[i]].loss} image={usableBattlers[battlersIndexes[i]].image} battlerClassName={"left align"}></Battler>) : <img key={i} className="default-img left-one" src=".\Images\transformericon.jpg"></img>}
                        </div>
                    ))}
                </div>   

                <b className='versus-text'>VS</b>

                <div className="battler-grid-right">
                    {Array(battleModeNumber + 1).fill(0).map((any, i) => ( 
                        <select key={i} className="battler-select" onChange={(event) => HandleSelectBattler(battleModeNumber + 1 + i, event)} value={battlersValues[battleModeNumber + 1 + i]}>
                            <option key={i}>Select your Battler</option>
                            {usableBattlers.map((battler, i) => (<option key={i} value={battler.name}>{battler.name}</option>))}
                        </select>
                    ))}
                    {Array(battleModeNumber + 1).fill(0).map((any, i) => ( 
                        <div key={i} className="default-img align">
                            {usableBattlers[battlersIndexes[battleModeNumber + 1 + i]] != null ? (<Battler key={i} name={usableBattlers[battlersIndexes[battleModeNumber + 1 + i]].name} faction={usableBattlers[battlersIndexes[battleModeNumber + 1 + i]].faction} winloss={usableBattlers[battlersIndexes[battleModeNumber + 1 + i]].win + "-" + usableBattlers[battlersIndexes[battleModeNumber + 1 + i]].loss} image={usableBattlers[battlersIndexes[battleModeNumber + 1 + i]].image} battlerClassName={"right"}></Battler>) : <img className="default-img right" src=".\Images\transformericon.jpg"></img>}
                        </div>
                    ))}
                </div>
                <button className="fight-button" onClick={() => StartBattle(battleModeNumber)}>FIGHT</button>
                <i className="status-text">{statusText}</i>
            </div>
        </div>
    );
}

export default App;