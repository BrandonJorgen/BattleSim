import "./Battler.css"

interface BattlerProps {
    name: string,
    faction?: string,
    winloss: string,
    image: string,
    battlerClassName: string,
}

export default function Battler({ name, faction, winloss, image, battlerClassName }: BattlerProps) {

    return (
        <div className={'battler ' + battlerClassName}>
            <img className="battler-img" src={image}></img>
            <b className="battler-name">{name}</b>
            <i className="battler-faction">{faction}</i>
            <i className="battler-winloss">{winloss}</i>
        </div>
    )
}