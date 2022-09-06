import React, { useEffect, useState } from 'react'
import '../sytles/Home.css'
import { useNavigate } from 'react-router-dom';
import { Database, get, getDatabase, onValue, orderByChild, ref } from 'firebase/database'
import  {app, auth, db}  from '../firebase/firebase';
import { Coordenadas } from './Coordenadas';

export const Home = () => {

    const [datos, setDatos] = useState([])
    const [todo, setTodo] = useState("");
    const [uid, setUid]= useState("")
    const [localizaciones, setLocalizaciones] = useState([])
    const [local, setLocal] = useState([]);

    const [arreglo, setArreglo] = useState([])
    const todos = [];
    const navigate = useNavigate();

    const [modalOn, setModalOn] = useState()
    const [choice, setChoice] = useState()

    const returnBtn = () => {
        auth.signOut();
        navigate('/login')
    }

    const Coordinates = () =>{
        navigate('/ubicacion',{uid:uid})
    }

    async function ViewInformation (){
        //setTodos([])
        const query = ref(db,"Users");
        await onValue(query,(snapshot)=>{
        const data = snapshot.val();
        //console.log('esto es la data',data)
        if(data!==null){
            Object.values(data).map((todo)=>{
                todos.push(todo)
            })
        }

        //console.log(todos)
        setDatos(todos)
        //console.log(datos)
    })
    }

    async function ViewArreglo (uid){
        //setTodos([])
        const query = ref(db,"Cyclists/"+uid+"/Localization");
        await onValue(query,(snapshot)=>{
        const data = snapshot.val();
        //console.log('esto es la data',data)
        if(data!==null){
            Object.values(data).map((localizacion)=>{
                localizaciones.push(localizacion)
            })
        }

        //console.log(todos)
        setLocal(localizaciones)
        //console.log(datos)
    })
    }

    useEffect(()=>{
        ViewInformation();
 
     },[])


     const Clicked = () =>{
        setModalOn(true);
     }

    return (

        <div className='flex flex-row items-center justify-around h-screen body-card bg-slate-400'>

    <div className='flex flex-col space-y-4 '>
    <div className=' card-general'>
    <div className='flex flex-row items-center justify-center p-4 leading-normal bg-white border border-gray-400 rounded lg:border-gray-400'>

        {modalOn && (<Coordenadas setModalOn={setModalOn} setChoice={setChoice} uid={uid}/>)}
         <table id="conten-table">
            <thead>
            <tr>
                <th>
                Nombre
                </th>
                <th>
                Celular
                </th>
                <th>
                Age
                </th>
                <th>
                Team
                </th>
            </tr>
            </thead>
            <tbody >
                {datos.map((item)=>(
                    <tr key={item.uid}>
                        <td>{item.completeName}</td>
                        <td>{item.cellPhone}</td>
                        <td>{item.age}</td>
                        <td>{item.team}</td>
                        <td><button
                        className='px-4 py-2 font-bold text-gray-800 bg-gray-300 rounded-l hover:bg-gray-400'
                        onClick={()=>{Clicked();setUid(item.uid)}}>Ver Ubicación</button></td>
                    </tr>
                ))}
            </tbody>
        </table>
        
                </div>
                
                </div>
             <div className='flex items-center justify-center'>

                <button onClick={()=>returnBtn()} className="w-64 px-4 py-2 font-bold text-white bg-blue-500 rounded-lg hover:bg-blue-700"> SALIR </button>
             </div>
                </div>



        </div>
    )
}