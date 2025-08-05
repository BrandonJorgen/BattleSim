import "./Battler.css"

interface BattlerProps {
    name: string,
    faction: string,
    winloss: string,
    image: string,
}

export default function Battler({ name, faction, winloss, image }: BattlerProps) {

    return (
        <div className={'battler'}>
            <img className="battler-img" src={image}></img>
            <b className="battler-name">{name}</b>
            <i className="battler-faction">{faction}</i>
            <i className="battler-winloss">{winloss}</i>
        </div>
    )
}