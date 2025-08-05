import { useState, useEffect } from 'react'
import './TeamGrid.css'
import Battler from '../Battler/Battler'
import type { IBattler } from '../../Interfaces/IBattler.ts'

interface TeamGridProps {
    battleType: number,
    battleCount: number, //Used to determine if a fight has been started
    battlers: never[],
    usableBattlers: IBattler[],
    teamSide: string,
    handleBattlerSelection: (indexes:number[], side:string) => void
}

export default function TeamGrid({ battleType, battlers, usableBattlers, teamSide, handleBattlerSelection }: TeamGridProps) {
    const [selectedBattlers, setSelectedBattlers] = useState([])
    const [battlersValues, setBattlersValues] = useState<string[]>([])
    const [battlersIndexes, setBattlersIndexes] = useState<number[]>([])

    const HandleSelectBattler = (slot: number, event: React.ChangeEvent<HTMLSelectElement>) => {
        let indexes: number[] = []
        let values: string[] = []
        values = battlersValues;
        values[slot] = event.target.value;
        setBattlersValues(values);
        const tempSelectedBattlers = [...selectedBattlers];

        if (event.target.value == "Select your Battler") {
            ResetBattlerSlot(slot);
            indexes = battlersIndexes;
            indexes[slot] = -1;
            setBattlersIndexes(indexes);
            setBattlersValues(values);
            tempSelectedBattlers[slot] = battlers[-1];
            setSelectedBattlers(tempSelectedBattlers);
        } else {
            for (let i = 0; i <= battlers.length - 1; i++) {
                if (usableBattlers[i].name == event.target.value) {
                    indexes = battlersIndexes;
                    indexes[slot] = i;
                    setBattlersIndexes(indexes);
                    tempSelectedBattlers[slot] = battlers[i];
                    setSelectedBattlers(tempSelectedBattlers);
                }
            }
        }
    }

    //wait for selected battlers state so we can send the data to the back end
    useEffect(() => {
        if (selectedBattlers.length > 0) {
            for (let i = 0; i <= battleType; i++) {
                if (battlersIndexes[i] == null)
                    battlersIndexes[i] = -1;
            }

            handleBattlerSelection(battlersIndexes, teamSide);
        }
    }, [selectedBattlers]);

    //Wait for battlers state so we can update the stats of the selected battlers
    useEffect(() => {
        if (selectedBattlers.length > 0) {
            const tempSelectedBattlers = [...selectedBattlers];

            for (let i = 0; i <= selectedBattlers.length - 1; i++) {
                for (let o = 0; o <= battlers.length - 1; o++) {
                    if (battlersIndexes[i] == o) {
                        tempSelectedBattlers[i] = battlers[o];
                    }
                }
            }

            setSelectedBattlers(tempSelectedBattlers);
        }

    }, [battleType])

    async function ResetBattlerSlot(slot: number) {
        await fetch(`battlesimulator/ResetBattlerSlot`, {
            method: 'POST',
            body: JSON.stringify(slot),
            headers: {
                'Content-Type': 'application/json',
            },
        });
    }

    return (
        <div className={"team-grid " + teamSide} >
            {Array(battleType + 1).fill(0).map((_any, i) => (
                <select key={i} className="battler-select" onChange={(event) => HandleSelectBattler(i, event)} value={battlersValues[i]}>
                    <option key={i} value="Select your Battler">Select your Battler</option>
                    {usableBattlers.map((battler, i) => (<option key={i} value={battler.name}>{battler.name}</option>))}
                </select>
            ))}
            {Array(battleType + 1).fill(0).map((_any, i) => (
                <div key={i} className="align">
                    {usableBattlers[battlersIndexes[i]] != null ? (<Battler key={i} name={usableBattlers[battlersIndexes[i]].name} faction={usableBattlers[battlersIndexes[i]].faction} winloss={usableBattlers[battlersIndexes[i]].win + "-" + usableBattlers[battlersIndexes[i]].loss} image={usableBattlers[battlersIndexes[i]].image}></Battler>) : <img key={i} className="default-img left-one" src=".\Images\transformericon.jpg"></img>}
                </div>
            ))}
        </div>
    )
} 