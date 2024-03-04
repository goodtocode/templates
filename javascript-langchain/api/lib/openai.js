import { ChatOpenAI } from 'langchain/chat_models/openai'
import { OpenAIEmbeddings } from 'langchain/embeddings/openai'

<<<<<<< HEAD
const openaiKey = 'API-KEY'
=======
    const openaiKey = 'OPENAPI_KEY'
>>>>>>> main

export const llm = new ChatOpenAI({
  openAIApiKey: openaiKey,
  modelName: 'gpt-3.5-turbo',
  temperature: 0.9,
})

export const embeddings = new OpenAIEmbeddings({
  openAIApiKey: openaiKey,
})