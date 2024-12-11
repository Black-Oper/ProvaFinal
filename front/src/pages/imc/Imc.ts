import { Aluno } from '../Aluno';

export interface Imc {
    id : number;
    resultimc : Float32Array;
    altura : Float32Array;
    peso : Float32Array;
    classificacao : string;
    datacriacao : string;
    aluno : Aluno;
    alunoid : number;
}