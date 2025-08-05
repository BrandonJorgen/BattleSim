import { useEffect, useState } from 'react';
import TeamGrid from '../TeamGrid/TeamGrid';
import './BattlerGrid.css'
import type { IBattler } from '../../Interfaces/IBattler.ts'

interface BattlerGridProps {
    battleType: number,
    battleCount: number
}

export default function BattlerGrid({ battleType, battleCount }: BattlerGridProps)
{
    const [battlers, setBattlers] = useState([])
    const [usableBattlers] = useState<IBattler[]>([])
    const [recievedBattlerIndexes] = useState<number[][]>([[],[]])

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
                    const tempItem = { name: "", faction: "", win: 0, loss: 0, image: "" };
                    tempItem.name = data[i].name;
                    tempItem.faction = data[i].faction;
                    tempItem.win = data[i].win;
                    tempItem.loss = data[i].loss;
                    tempItem.image = data[i].image;
                    usableBattlers.push(tempItem);
                }
            }
        }
    }

    const handleBattlerSelection = (selectedBattlerIndexes: number[], teamSide: string) => {
        if (teamSide == "left") //0 = left, 1 = right
            recievedBattlerIndexes[0] = selectedBattlerIndexes;
        else
            recievedBattlerIndexes[1] = selectedBattlerIndexes;

        for (let i = 0; i <= recievedBattlerIndexes.length - 1; i++)
        {
            for (let o = 0; o <= battleType; o++) {
                if (recievedBattlerIndexes[i].length - 1 < battleType)
                    if (recievedBattlerIndexes[i][o] == undefined || null)
                        recievedBattlerIndexes[i][o] = -1;
            }
        }

        UpdateBattlerChoices(recievedBattlerIndexes);
    }

    async function UpdateBattlerChoices(teamIndexesArray: number[][]) {
        await fetch(`battlesimulator/UpdateBattlers`, {
            method: 'POST',
            body: JSON.stringify(
                {
                    "teamIndexes": teamIndexesArray,
                    "battleMode": battleType
                }
            ),
            headers: {
                'Content-Type': 'application/json',
            },
        });
    }

    async function UpdateStats() {
        const response = await fetch('battlesimulator/UpdateStats')
        if (response.ok) {
            const data = await response.json();
            setBattlers(data);

            for (let i = 0; i <= data.length - 1; i++) {
                const tempItem = { name: "", faction: "", win: 0, loss: 0, image: "" };
                tempItem.name = data[i].name;
                tempItem.faction = data[i].faction;
                tempItem.win = data[i].win;
                tempItem.loss = data[i].loss;
                tempItem.image = data[i].image;
                usableBattlers[i] = tempItem;
            }
        }
    }

    useEffect(() => {
        if (battleCount > 0)
            UpdateStats();
    }, [battleCount])
    
    return (
        <div className="battler-grid">
            <TeamGrid key={0} battleType={battleType} battleCount={battleCount} battlers={battlers} usableBattlers={usableBattlers} teamSide={'left'} handleBattlerSelection={handleBattlerSelection} />
            <b className='versus-text'>VS</b>
            <TeamGrid key={1} battleType={battleType} battleCount={battleCount} battlers={battlers} usableBattlers={usableBattlers} teamSide={'right'} handleBattlerSelection={handleBattlerSelection} />
        </div>
    );
}