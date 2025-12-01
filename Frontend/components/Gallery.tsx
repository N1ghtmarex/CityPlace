import { Picture } from "@/types/picture";
import { use, useState } from "react";

interface Props {
  pictures: Picture[];
}

export default function Gallery({ pictures }: Props) {
    const [index, setIndex] = useState(0);
    const [currentPictureId, setCurrentPictureId] = useState(pictures[0].id)
    
    return (
        <div className="gallery-wrapper">
            <div className="gallery-container">
                <h1 className="gallery-title">Галерея изображений</h1>
                <div className="gallery-main-block">
                    <div className="left-row" onClick={() => {
                        if (index != 0) {
                            setIndex(index - 1);
                            setCurrentPictureId(pictures[index-1].id);
                        }
                    }}>
                        <span className={`left-arrow ${index == 0 ? 'hide' : ''}`}>❮</span>
                    </div>
                    <div className="main-picture">
                        <img src={`${process.env.NEXT_PUBLIC_API_URL}/${pictures[index]?.path}`}></img>
                    </div>
                    <div className="right-row" onClick={() => {
                        if (index + 1 < pictures.length) {
                            setIndex(index + 1);
                            setCurrentPictureId(pictures[index+1].id);
                        }
                    }}>
                        <span className={`right-arrow ${index == pictures.length - 1 ? 'hide' : ''}`}>❯</span>
                    </div>
                </div>

                <div className="preview-wrapper">
                    <div className="preview-grid">
                        { pictures
                            .slice(
                                Math.max(0, Math.min(
                                    Math.floor(index / 3),
                                    pictures.length - 5
                                )),
                                Math.min(
                                    Math.max(0, Math.min(
                                        Math.floor(index / 3), 
                                        pictures.length - 5
                                    )) + 5, 
                                    pictures.length
                                )
                            )
                            .map((picture, i) => (
                                <div className={`img-preview ${picture.id == currentPictureId ? 'current' : ''}`} key={picture.id}>
                                    <img src={`${process.env.NEXT_PUBLIC_API_URL}/${picture.path}`}></img>
                                </div>
                                )
                            )
                        }
                    </div>
                </div>
            </div>
        </div>
    )
}