pipeline {
    agent any
    stages {
        stage('Unit testing') {
            steps {
                sh 'docker-compose -f docker-compose.tests.yml run --rm Project /src/Backend/PS-Tests/test-script.sh'
                sh 'docker-compose -f docker-compose.tests.yml run --rm Project /src/Backend/MS-Tests/test-script.sh'
            }
        }
        stage('Build docker images') {
            steps {
                sh 'docker compose build'
            }
        }
        stage('Publish docker images') {
            steps {
                withCredentials([usernamePassword(credentialsId: 'Dockerhub', usernameVariable: 'DOCKER_USER', passwordVariable: 'DOCKER_PASS')]) 
                {
                    sh 'docker login -u $DOCKER_USER -p $DOCKER_PASS'
                    sh 'docker compose push'
                }
            }
        }
    }
}
