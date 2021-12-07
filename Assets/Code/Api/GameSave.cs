using SMB.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SMB.Api
{
    [Serializable]
    public class GameSave
    {
        [JsonProperty("character", NullValueHandling = NullValueHandling.Ignore)]
        public PlayerType Character;

        [JsonProperty("coins", NullValueHandling = NullValueHandling.Ignore)]
        public int Coins;

        [JsonProperty("lives", NullValueHandling = NullValueHandling.Ignore)]
        public int Lives;

        [JsonProperty("stagesUnlocked", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> StagesUnlocked;

        [JsonProperty("scoreTotal", NullValueHandling = NullValueHandling.Ignore)]
        public long ScoreTotal;

        public static GameSave CreateNew(PlayerType character)
        {
            return new GameSave()
            {
                Character = character,
                Coins = 0,
                Lives = 3,
                StagesUnlocked = new List<string>()
                {
                    "1-1"
                },
                ScoreTotal = 0
            };
        }
    }
}
