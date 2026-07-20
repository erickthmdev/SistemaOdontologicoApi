import http from 'k6/http';
import { sleep, check } from 'k6';

export const options = {
    stages: [
        { duration: '10s', target: 10 },  // sobe até 10 usuários em 10s
        { duration: '20s', target: 10 },  // mantém 10 usuários por 20s
        { duration: '10s', target: 0 },   // desce até 0
    ],
};

export default function () {
    const res = http.get('https://localhost:XXXX/api/SEU_ENDPOINT');

    check(res, {
        'status é 200': (r) => r.status === 200,
        'resposta rápida (<500ms)': (r) => r.timings.duration < 500,
    });

    sleep(1);
}