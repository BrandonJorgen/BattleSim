/* eslint-disable @typescript-eslint/no-unused-vars */
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
    const [battlerOneValue, setBattlerOneValue] = useState("")
    const [battlerTwoValue, setBattlerTwoValue] = useState("")
    const [battlerThreeValue, setBattlerThreeValue] = useState("")
    const [battlerFourValue, setBattlerFourValue] = useState("")
    const [battlerOne, setBattlerOne] = useState()
    const [battlerTwo, setBattlerTwo] = useState()
    const [battlerThree, setBattlerThree] = useState()
    const [battlerFour, setBattlerFour] = useState()
    const [battlerOneIndex, setBattlerOneIndex] = useState(-1)
    const [battlerTwoIndex, setBattlerTwoIndex] = useState(-1)
    const [battlerThreeIndex, setBattlerThreeIndex] = useState(-1)
    const [battlerFourIndex, setBattlerFourIndex] = useState(-1)
    const [statusText, setStatusText] = useState('Choose your battlers!')
    const [battleType, setBattleType] = useState("1v1")


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

    const HandleSelectBattlerOne = (event: React.ChangeEvent<HTMLSelectElement>) => {
        console.log(event.target.value)
        if (event.target.value == "Select your Battler") {
            ResetBattlerSlot(0);
            setBattlerOneIndex(-1);
            setBattlerOneValue(event.target.value);
        } else {
            for (let i = 0; i <= battlers.length - 1; i++) {
                if (usableBattlers[i].name == event.target.value) {
                    setBattlerOne(battlers[i]);
                    setBattlerOneIndex(i);
                    setBattlerOneValue(event.target.value);
                }
            }
        }
        
    }

    useEffect(() => {
        if (battlerOne != null) {
            UpdateBattlerOneChoice();
        }
    }, [battlerOne]);

    const HandleSelectBattlerTwo = (event: React.ChangeEvent<HTMLSelectElement>) => {
        if (event.target.value == "Select your Battler") {
            ResetBattlerSlot(1);
            setBattlerTwoIndex(-1);
            setBattlerTwoValue(event.target.value);
        } else {
            for (let i = 0; i <= battlers.length - 1; i++) {
                if (usableBattlers[i].name == event.target.value) {
                    setBattlerTwo(battlers[i]);
                    setBattlerTwoIndex(i);
                    setBattlerTwoValue(event.target.value);
                }
            }
        }
    }

    useEffect(() => {
        if (battlerTwo != null) {
            UpdateBattlerTwoChoice();
        }
    }, [battlerTwo]);

    const HandleSelectBattlerThree = (event: React.ChangeEvent<HTMLSelectElement>) => {
        if (event.target.value == "Select your Battler") {
            ResetBattlerSlot(2);
            setBattlerThreeIndex(-1);
            setBattlerThreeValue(event.target.value);
        } else {
            for (let i = 0; i <= battlers.length - 1; i++) {
                if (usableBattlers[i].name == event.target.value) {
                    setBattlerThree(battlers[i]);
                    setBattlerThreeIndex(i);
                    setBattlerThreeValue(event.target.value);
                }
            }
        }
    }

    useEffect(() => {
        if (battlerThree != null) {
            UpdateBattlerThreeChoice();
        }
    }, [battlerThree]);

    const HandleSelectBattlerFour = (event: React.ChangeEvent<HTMLSelectElement>) => {
        if (event.target.value == "Select your Battler") {
            ResetBattlerSlot(3);
            setBattlerFourIndex(-1);
            setBattlerFourValue(event.target.value);
        } else {
            for (let i = 0; i <= battlers.length - 1; i++) {
                if (usableBattlers[i].name == event.target.value) {
                    setBattlerFour(battlers[i]);
                    setBattlerFourIndex(i);
                    setBattlerFourValue(event.target.value);
                }
            }
        }
    }

    useEffect(() => {
        if (battlerFour != null) {
            UpdateBattlerFourChoice();
        }
    }, [battlerFour]);

    async function UpdateBattlerOneChoice() {
        await fetch(`battlesimulator/UpdateBattlerOne`, {
            method: 'POST',
            body: JSON.stringify(battlerOne),
            headers: {
                'Content-Type': 'application/json',
            },
        });
    }

    async function UpdateBattlerTwoChoice() {
        await fetch(`battlesimulator/UpdateBattlerTwo`, {
            method: 'POST',
            body: JSON.stringify(battlerTwo),
            headers: {
                'Content-Type': 'application/json',
            },
        });
    }

    async function UpdateBattlerThreeChoice() {
        await fetch(`battlesimulator/UpdateBattlerThree`, {
            method: 'POST',
            body: JSON.stringify(battlerThree),
            headers: {
                'Content-Type': 'application/json',
            },
        });
    }

    async function UpdateBattlerFourChoice() {
        await fetch(`battlesimulator/UpdateBattlerFour`, {
            method: 'POST',
            body: JSON.stringify(battlerFour),
            headers: {
                'Content-Type': 'application/json',
            },
        });
    }

    async function StartBattle1v1() {
        const response = await fetch('battlesimulator/Battle1v1')
        if (response.ok) {
            const data = await response.text();
            setStatusText(data);
            UpdateStats();
        }
    }

    async function StartBattle2v2() {
        const response = await fetch('battlesimulator/Battle2v2')
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
            </select>
            {battleType == "1v1" ? (
                <div className="battler-grid onevone">
                    <select className="battler-select left" onChange={HandleSelectBattlerOne} value={battlerOneValue}>
                        <option value="Select your Battler">Select your Battler</option>
                        {usableBattlers.map((battler, i) => (<option key={i} value={battler.name}>{battler.name}</option>))}
                    </select>
                    {usableBattlers[battlerOneIndex] != null ? (<Battler name={usableBattlers[battlerOneIndex].name} faction={usableBattlers[battlerOneIndex].faction} winloss={usableBattlers[battlerOneIndex].win + "-" + usableBattlers[battlerOneIndex].loss} image={usableBattlers[battlerOneIndex].image} battlerClassName={"left"}></Battler>) : <img className="default-img left-one" src=".\Images\transformericon.jpg"></img>}
                    <b className='versus-text'>VS</b>
                    <select className="battler-select right" onChange={HandleSelectBattlerTwo} value={battlerTwoValue}>
                        <option>Select your Battler</option>
                        {usableBattlers.map((battler, i) => (<option key={i} value={usableBattlers[i].name}>{battler.name}</option>))}
                    </select>
                    {usableBattlers[battlerTwoIndex] != null ? (<Battler name={usableBattlers[battlerTwoIndex].name} faction={usableBattlers[battlerTwoIndex].faction} winloss={usableBattlers[battlerTwoIndex].win + "-" + usableBattlers[battlerTwoIndex].loss} image={usableBattlers[battlerTwoIndex].image} battlerClassName={"right"}></Battler>) : <img className="default-img right" src=".\Images\transformericon.jpg"></img>}
                    <button className="fight-button" onClick={StartBattle1v1}>FIGHT</button>
                    <i className="status-text">{statusText}</i>
                </div>
            ) : battleType == "2v2" ? (
                <div className="battler-grid twovtwo">
                        <select className="battler-select left-one" onChange={HandleSelectBattlerOne} value={battlerOneValue}>
                        <option>Select your Battler</option>
                            {usableBattlers.map((battler, i) => (<option key={i} value={usableBattlers[i].name}>{battler.name}</option>))}
                    </select>
                        {usableBattlers[battlerOneIndex] != null ? (<Battler name={usableBattlers[battlerOneIndex].name} faction={usableBattlers[battlerOneIndex].faction} winloss={usableBattlers[battlerOneIndex].win + "-" + usableBattlers[battlerOneIndex].loss} image={usableBattlers[battlerOneIndex].image} battlerClassName={"left-one"}></Battler>) : <img className="default-img left-one" src=".\Images\transformericon.jpg"></img>}
                        <select className="battler-select left-two" onChange={HandleSelectBattlerTwo} value={battlerTwoValue}>
                        <option>Select your Battler</option>
                            {usableBattlers.map((battler, i) => (<option key={i} value={usableBattlers[i].name}>{battler.name}</option>))}
                    </select>
                        {usableBattlers[battlerTwoIndex] != null ? (<Battler name={usableBattlers[battlerTwoIndex].name} faction={usableBattlers[battlerTwoIndex].faction} winloss={usableBattlers[battlerTwoIndex].win + "-" + usableBattlers[battlerTwoIndex].loss} image={usableBattlers[battlerTwoIndex].image} battlerClassName={"left-two"}></Battler>) : <img className="default-img left-two" src=".\Images\transformericon.jpg"></img>}
                    <b className='versus-text twovtwo'>VS</b>
                        <select className="battler-select right-one" onChange={HandleSelectBattlerThree} value={battlerThreeValue}>
                        <option>Select your Battler</option>
                            {usableBattlers.map((battler, i) => (<option key={i} value={usableBattlers[i].name}>{battler.name}</option>))}
                    </select>
                        {usableBattlers[battlerThreeIndex] != null ? (<Battler name={usableBattlers[battlerThreeIndex].name} faction={usableBattlers[battlerThreeIndex].faction} winloss={usableBattlers[battlerThreeIndex].win + "-" + usableBattlers[battlerThreeIndex].loss} image={usableBattlers[battlerThreeIndex].image} battlerClassName={"right-one"}></Battler>) : <img className="default-img right-one" src=".\Images\transformericon.jpg"></img>}
                        <select className="battler-select right-two" onChange={HandleSelectBattlerFour} value={battlerFourValue}>
                        <option>Select your Battler</option>
                            {usableBattlers.map((battler, i) => (<option key={i} value={usableBattlers[i].name}>{battler.name}</option>))}
                    </select>
                        {usableBattlers[battlerFourIndex] != null ? (<Battler name={usableBattlers[battlerFourIndex].name} faction={usableBattlers[battlerFourIndex].faction} winloss={usableBattlers[battlerFourIndex].win + "-" + usableBattlers[battlerFourIndex].loss} image={usableBattlers[battlerFourIndex].image} battlerClassName={"right-two"}></Battler>) : <img className="default-img right-two" src=".\Images\transformericon.jpg"></img>}
                    <button className="fight-button-twovtwo" onClick={StartBattle2v2}>FIGHT</button>
                    <i className="status-text-twovtwo">{statusText}</i>
                </div>
            ): null}
        </div>
    );
}

export default App;