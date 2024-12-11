import React, { useEffect, useState } from "react";
import { Imc } from '../imc/Imc'
import { error } from "console";

const Listar : React.FC = () => {
    const [imcs, setImcs] = useState<Imc[]>([]);

    useEffect(() => {
        fetch('http://localhost:5161/api/imc/listar')
            .then(response => {
                if(!response.ok) {
                    throw new Error('Erro na requisição: ' + response.statusText);
                }
                return response.json();
            })
            .then(data => {
                setImcs(data);
            })
            .catch(error => {
                console.error('Erro:',error);
            });
    }, []);

    return (
        <div>
            <h1>LISTA DE IMC'S</h1>
            <table>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Resultado Imc</th>
                        <th>Peso</th>
                        <th>Altura</th>
                        <th>Classificação</th>
                        <th>Data de Criação</th>
                        <th>ID Aluno</th>
                        <th>Nome</th>
                        <th>Sobrenome</th>
                    </tr>
                </thead>
                <tbody>
                    {imcs.map(imc => (
                        <tr key={imc.id}>
                            <td>{imc.id}</td>
                            <td>{imc.resultimc}</td>
                            <td>{imc.peso}</td>
                            <td>{imc.altura}</td>
                            <td>{imc.classificacao}</td>
                            <td>{imc.datacriacao}</td>
                            <td>{imc.aluno.id}</td>
                            <td>{imc.aluno.nome}</td>
                            <td>{imc.aluno.sobrenome}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default Listar;