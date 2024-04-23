pipeline {
    agent any
    stages {
        stage('Unit testing') {
            steps {
                sh 'docker-compose -f docker-compose.tests.yml run -u root --rm ps'
                sh 'docker-compose -f docker-compose.tests.yml run -u root --rm ms'
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
