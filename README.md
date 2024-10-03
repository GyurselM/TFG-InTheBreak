# In The Break

## Definición del proyecto

El proyecto habrá varias formas de definirlo, desde en un par de palabras como la famosa
técnica del “Elevator Speech” hasta una presentación más larga que explica de forma
extensa lo que se espera del proyecto, que se busca con el y demás aspectos importantes
en el desarrollo del juego.

## Elevator Speech

La primera definicion sera el “Elevator Speech” en la que venderemos con una frase corta
nuestro proyecto, en este caso la frase con la que intentaremos vender nuestro producto
será: “In the break, vuelve a jugar en el patio”
La explicación de esta frase tiene que ver con su estilo y a lo que intenta apelar que es a la
nostalgia de las bromas y los juegos que se nos ocurrían en el recreo cuando eramos
pequeños con nuestros amigos.

## Resumen

La segunda definición del proyecto es una presentación larga en la que explicaremos
extensamente varios puntos importantes del juego, en el que incluso habría una exposición
con un powerpoint, apoyándose sobre todo para explicar el estilo tan característico del
proyecto. A continuación se explican los puntos de los que se hablarán, para que además
de servir como guión, explique la idea del juego.

#### In the break

El juego se basa en volver a ser un adolescente que nació en los late 90 y early
2000 donde la tecnología empezó a mejorar de forma exponencial, aparecieron
redes sociales, empezaron a aparecer los primeros smartphones y el mundo se
abría ante todos de forma masiva. Con ello aparecieron los memes de la forma que
los conocemos hoy en día y empezamos a llevar bromas internas de grupos de
chicos y chicas al mundo exterior haciendo comunidades de todos los temas
imaginables.

A la vez en los 90 aparecieron juegos como Yu-Gi-Oh y Magic: The Gathering, que
cuando estos se volvieron virales los adolescentes en los que nos basamos ya
podían jugar al juego porque había llegado a todos los países.

Uniéndose a los dos pilares anteriores también aparece las experiencias de
adolescente de los desarrolladores en los que dibujamos continuamente en el
cuaderno, porque a veces una clase de historia a las 9 de la mañana no era lo que
queríamos y decidimos evadirnos a través de dibujar cosas en el cuaderno hasta
llenarlo de garabatos sin sentido, que enseñamos al compañero que teníamos al
lado para ver si se reía.

Mezclando estas 3 ideas anteriores sale la idea de hacer un juego que a traves de
su estilo te haga volver a sentir y a esbozar esa sonrisa de ver un dibujo tonto en un
cuaderno mientras esperabas que sonase la campana para irte al recreo a jugar a
un juego de cartas con las reglas mas disparatadas y sin sentido que se te ocurrian.

Para ello en el proyecto los dos aspectos más importantes son el estilo artístico de
este y su jugabilidad.

Como se ha expresado anteriormente este estilo recuerda a dibujos que haría
cualquier adolescente en su cuaderno siendo este estilo muy cartoon, llegando al
estilo “Ignorant” que se puede ver incluso en tatuajes. En el estilo “Ignorant” no se
buscan trazos perfectos si no que líneas simples, palabras mal escritas,
homenajeando el graffiti primitivo de los 90.

También es importante los dibujos que se harán en cuanto a contenido, apelando a
cosas que pueden estar dentro del colegio, a “memes” de gran difusión y a
pequeñas bromas de humor interno que se crearán en torno al juego y su estilo,
pudiendo aparecer cartas como: La cocinera del comedor, el bocata y zumo que te
comes en el recreo, a memes como el “nyan cat” y demas.

### Demo
[Video Demo](https://youtu.be/FMt1qkZGbHU)

## Reto tecnicos

El mayor problema que tuvimos a la hora de hacer el proyecto era la conexión con la BBDD (Utilizando Supabase)
a Unity para poder cargar los datos y las imagenes al juego y asi tener nuestras cartas listas para jugar y así nuestro juego puede estar en continuo evolución simplemente 
seria meter mas cartas y con eso ya estaría en el juego y la solución que tuvimos era esta:

>Este método es la petición web que accede a supabase y coge los dados de la tabla cards
 llega aquí como un array de string lo que hemos hecho es crear un script llamado CardData 
 que lo que hace es que nos separa todo por cachos es decir campo nombre y su valor, es la mejor manera para poder luego cargar en las cartas.

```csharp
    IEnumerator GetText()
    {
        string APIKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Imt0ZWhzeG9saHF1bXRudnBxb2NlIiwicm9sZSI6ImFub24iLCJpYXQiOjE2OTYzMTc1MjIsImV4cCI6MjAxMTg5MzUyMn0.xB3p8XVTAFHwBH6Q1callbfUEZNHqWkJFnxj5G6fucE";
        string APIKeyP = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
        string url = "https://ktehsxolhqumtnvpqoce.supabase.co/rest/v1/cards?select=*&order=ca_id.asc&apikey=" + APIKey;
        
        UnityWebRequest supabase = UnityWebRequest.Get(url);
        supabase.SetRequestHeader("Authorization","Bearer" + APIKeyP);
        yield return supabase.SendWebRequest();
        
        print(supabase.result);
        
        if (supabase.result != UnityWebRequest.Result.Success){Debug.LogError("ERROR: "+supabase.error);}
        else{
        
            string jsonR = supabase.downloadHandler.text;
            CardData.ParseJson(jsonR);
            Debug.Log(jsonR);
            
            cards = CardData.cartas;
            BBDD.Cartas = cards;
            Debug.Log("Reconstructed CardData: " + CardData.cartas);
            LoadingScreen.SetActive(false);
        
        }
    }
```
Y el segundo reto técnico que teníamos era guardar datos y pasar a otra escena para poder manejar los datos guardados asi que se nos ocurrió realizar el Singleton.
```csharp
    public class GameManager : MonoBehaviour
    {
        // Lista que deseas almacenar en el singleton
        private List<int> mazoJugar = new List<int>();
    
        // Instancia estática del GameManager
        private static GameManager _instance;

        // Nivel de dificultad
        private int difficultyLevel = 0;

        // Mejoras
        private int lifeUpgrade = 0;
        private int manaUpgrade = 0;
        private int spriteId = 0;
    
        // Propiedad estática para acceder a la instancia del GameManager
        public static GameManager Instance
        {
            get
            {
                // Si no hay una instancia existente, crea una nueva
                if (_instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    _instance = go.AddComponent<GameManager>();
                    DontDestroyOnLoad(go); // No destruye el objeto GameManager al cargar una nueva escena
                }
                return _instance;
            }
        }
    
        // Método para agregar un dato a la lista
        public void AddToMazoJugar(int dato)
        {
            mazoJugar.Add(dato);
        }
    
        // Método para obtener la lista
        public List<int> GetMazoJugar()
        {
            return mazoJugar;
        }
    
        public void SetMazoJugar(List<int> newDeck)
        {
           mazoJugar = newDeck;

        }
        // Método para obtener el nivel de dificultad
        public int GetDifficultyLevel()
        {
            return difficultyLevel;
        }
    
        // Método para establecer el nivel de dificultad
        public void SetDifficultyLevel(int level)
        {
            difficultyLevel = level;
        }
    
        //Life upgrade
        public int GetLifeUpgrade()
        {
            return lifeUpgrade;
        }
        public void SetLifeUpgrade(int upgrade)
        {
            lifeUpgrade += upgrade;
        }
    
        //Mana upgrade
        public int GetManaUpgrade()
        {
            return manaUpgrade;
        }
        public void SetManaUpgrade(int upgrade)
        {
            manaUpgrade += upgrade;
        }
        public int GetSpriteId()
        {
            return spriteId;
        }
        public void SetSpriteId(int id)
        {
            spriteId = id;
        }
    }
```
