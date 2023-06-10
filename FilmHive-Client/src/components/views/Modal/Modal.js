// import React from "react";
// import "./Modal.css"
// const [search, setSearch] = useState("")

//     const Modal = props=>{
//         if(!props.show)
//         return null
//     }

//     useEffect(() => {
//         fetch(`https://localhost:7211/search?title=${title}`)
//           .then((response) => response.json())
//           .then((data) => {
//             setAllBooks(data);
          
//             console.log(data);
//           });
//       }, [title]);
       
//     return (
//         <div className="Modal">
//             <div className="modal-content">
//                 <div className="modal-header">
//                     <h4 className="modal-title">Modal Title</h4>
//                 </div>
//                 <div className="modal-body">
//                 text
//                 </div>
//                 <div className="modal-footer">
//                     <button onClick={props.onClose} className="button">Close</button>
//                 </div>
//             </div>
//         </div>
//     )
    
//     export default Modal