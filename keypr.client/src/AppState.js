import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  activeProfile: {},
  activeKeeps: [],
  activeVaults: [],
  activeVault: {},
  keep: {},
  vault: {},
  keeps: [],
  vaultKeeps: []
  // userVaults: []
})
