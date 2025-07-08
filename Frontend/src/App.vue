<script setup>
    import { ref, onMounted } from 'vue'
    import axios from 'axios'

    const dados = ref([])
    const erro = ref(null)

    onMounted(async () => {
        try {
            const resposta = await axios.get('https://localhost:7290/Categoria/BuscarCategoria')
            dados.value = resposta.data
        } catch (e) {
            erro.value = e.message
        }
    })
</script>

<template>
    <div>
        <h1>Teste de API</h1>
        <div v-if="erro">Erro: {{ erro }}</div>
        <pre v-else>{{ dados }}</pre>
    </div>
</template>